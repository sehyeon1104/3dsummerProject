using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : LivingEntity
{
    [SerializeField]
    Slider healthSlider;  //ü���� ǥ���� �����̴�
    [SerializeField]
     AudioClip deathClip; // ��� �Ҹ�
    [SerializeField]      
    AudioClip hitClip;    //�ǰ� �Ҹ�
    [SerializeField]
    AudioClip itemPickupClip; // ������ ���� �Ҹ�

    AudioSource playerAudio; //�÷��̾� �Ҹ� �����
    Animator playerAnimator; // �÷��̾� �ִϸ�����
     
    PlayerMovement playerMovement; //�÷��̾� ������ ������Ʈ
    PlayerShooter playerShooter;   //�÷��̾� ���� ������Ʈ
    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerAudio =GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();
    }
    public override void Die()
    {
        //�θ��� Die ����
        base.Die();

        // ü�� �����̴� ��Ȱ��ȭ
        healthSlider.gameObject.SetActive(false);

        //����� ���
        playerAudio.PlayOneShot(deathClip);

        //Die Ʈ���� �߻�
        playerAnimator.SetTrigger("Die");

        //�÷��̾� �ҽ��� �޴� ������Ʈ ��Ȱ��ȭ
        playerMovement.enabled = false;
        playerShooter.enabled = false;
    }
    protected override void OnEnable()
    {
        //�θ�(LivingEntity)��  OnEnable ����
        base.OnEnable();

        //ü���� (�����̴�)
        healthSlider.gameObject.SetActive(true);
        healthSlider.maxValue = startingHealth;
        healthSlider.value = health;

        //�÷��̾� ������ �޴� ������Ʈ Ȱ��ȭ
        playerMovement.enabled = true;
        playerShooter.enabled = true;
    }
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if(!dead)
        {
            playerAudio.PlayOneShot(hitClip);
        }
        //�θ��� onDamge����
        base.OnDamage(damage, hitPoint, hitNormal);

        // ���ŵ� ü���� �����̴��� �ݿ�
        healthSlider.value = health;
    }
    public override void RestoreHealth(float newHealth)
    {
        //�θ��� RestoreHealth
        base.RestoreHealth(newHealth);

        healthSlider.value = health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!dead)
        {
            //�浹�� �������κ���  Item ������Ʈ ��������
            IItem item = other.GetComponent<IItem>(); 
             //item �������� ����
            if(item != null)
            {
                item.Use(gameObject);

                playerAudio.PlayOneShot(itemPickupClip);
            }
        }
    }
}
