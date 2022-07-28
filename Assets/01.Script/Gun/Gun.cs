using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //���� ���� 
    public enum State
    {
        Ready, //�߻� �غ� 
        Empty, //źâ�� �� ����
        Reloading // ��������
    }
    public State state { get; private set; }

    public Transform fireTransform; //ź���� �߻� ��ġ

    public ParticleSystem muzzleFlashEffect; // �ѱ� ȭ�� ȿ��
    public ParticleSystem shellEjectEffect; // ź�� ���� ȿ��

    public GunData gunData; // ���� ���� ������

    LineRenderer bulletLineRenderer; //ź�� ����

    AudioSource audioSource; // �� �Ҹ�

    WaitForSeconds wait = new WaitForSeconds(0.03f);
    

    float fireDistacne = 50f; //���� �Ÿ�
    float lastFireTime; // ������ �߻� ����

    public int ammoRemain = 100; // ���� ��ü ź��
    public int magAmmo = 0; // ���� źâ�� �����ִ� ź��

   
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();
         
        //���� �� ���� 2���� ���� / ������ ��Ȱ��ȭ
        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
    }
    private void OnEnable()
    {
        //��ü ���� ź�� �� �ʱ�ȭ
        ammoRemain = gunData.startAmmoReamain;

        //���� źâ�� ���� ä���
        magAmmo = gunData.magCapacity;

        state = State.Ready;

        lastFireTime = 0.0f;
    }

    //�߻�
    public void Fire()
    {
        if(state == State.Ready&&Time.time>=lastFireTime+gunData.timeBetFire)
        {
            lastFireTime = Time.time;

            Shot();
        }
    }

    void Shot()
    {
       
        //����ĳ��Ʈ�� ���� �浹 ������ �����ϴ� �����̳�
        RaycastHit hit;
        //ź���� ���� ���� ������ ����
        Vector3 hitPos = Vector3.zero;

        //���� ĳ��Ʈ 
        if(Physics.Raycast(fireTransform.position,fireTransform.forward,out hit,10f))
        {
            hitPos = hit.point;

            //���̰� � ��ü�� �浹�� �߻� ���� ��
            IDamagable target = hit.collider.GetComponent<IDamagable>();

            if(target != null)
            {
                target.OnDamage(gunData.damage,hitPos,hitPos);
            }
        }
        else
        {
            hitPos = fireTransform.position + fireTransform.forward * fireDistacne;
        }

        //�߻� ����Ʈ ��� ���� 
        StartCoroutine(ShotEffect(hitPos));
        magAmmo--;

        if(magAmmo<= 0)
        {
            //źâ�� ���� ź���� ���ٸ� ���� ���� ���¸� Empty�� ����
            state = State.Empty;
        }
    }
    IEnumerator ShotEffect(Vector3 hitPos)
    {
        //�ѱ� ȿ�� 
        muzzleFlashEffect.Play();
        // ź�� ���� ȿ�� ��� 
        shellEjectEffect.Play();

        audioSource.PlayOneShot(gunData.shotClip);

        bulletLineRenderer.SetPosition(0, fireTransform.position);
        bulletLineRenderer.SetPosition(1,hitPos);
        bulletLineRenderer.enabled = true;

        yield return wait;

        //���� ������ ��Ȱ��ȭ
        bulletLineRenderer.enabled = false;
    }

    //������ �õ�
    public bool Reload()
    {
        if(state==State.Reloading||ammoRemain<=0||magAmmo>=gunData.magCapacity)
        {
            return false;
        }

        //������ ó�� ����
        StartCoroutine(ReloadRoutine());

        return true;
    }
    IEnumerator ReloadRoutine()
    {
        state = State.Reloading;
        //������ �Ҹ� ���
       audioSource.PlayOneShot(gunData.reloadClip);

        //������ �ҿ� �ð���ŭ ��ٸ�

        yield return new WaitForSeconds(gunData.reloadTime);

        int ammoToFill =gunData.magCapacity-magAmmo;
        if(ammoRemain<ammoToFill)
        {
           ammoToFill = ammoRemain;
        }
        magAmmo += ammoToFill;
        ammoRemain -= ammoToFill;

        state = State.Ready;
    }
}
