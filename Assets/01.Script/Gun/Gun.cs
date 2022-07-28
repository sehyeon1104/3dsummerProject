using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //총의 상태 
    public enum State
    {
        Ready, //발사 준비 
        Empty, //탄창이 빈 상태
        Reloading // 재장전중
    }
    public State state { get; private set; }

    public Transform fireTransform; //탄알의 발사 위치

    public ParticleSystem muzzleFlashEffect; // 총구 화염 효과
    public ParticleSystem shellEjectEffect; // 탄피 배출 효과

    public GunData gunData; // 총의 현재 데이터

    LineRenderer bulletLineRenderer; //탄알 궤적

    AudioSource audioSource; // 총 소리

    WaitForSeconds wait = new WaitForSeconds(0.03f);
    

    float fireDistacne = 50f; //사정 거리
    float lastFireTime; // 마지막 발사 시점

    public int ammoRemain = 100; // 남은 전체 탄알
    public int magAmmo = 0; // 현재 탄창에 남아있는 탄알

   
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();
         
        //사용랄 점 갯수 2개로 설정 / 렌더러 비활성화
        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
    }
    private void OnEnable()
    {
        //전체 예비 탄알 양 초기화
        ammoRemain = gunData.startAmmoReamain;

        //현재 탄창을 가득 채우기
        magAmmo = gunData.magCapacity;

        state = State.Ready;

        lastFireTime = 0.0f;
    }

    //발사
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
       
        //레이캐스트에 의한 충돌 정보를 저장하는 컨테이너
        RaycastHit hit;
        //탄알이 맞은 곳을 저장할 변수
        Vector3 hitPos = Vector3.zero;

        //레이 캐스트 
        if(Physics.Raycast(fireTransform.position,fireTransform.forward,out hit,10f))
        {
            hitPos = hit.point;

            //레이가 어떤 물체와 충돌이 발생 했을 때
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

        //발사 이펙트 재생 시작 
        StartCoroutine(ShotEffect(hitPos));
        magAmmo--;

        if(magAmmo<= 0)
        {
            //탄창에 남은 탄알이 없다면 총의 현재 상태를 Empty로 갱신
            state = State.Empty;
        }
    }
    IEnumerator ShotEffect(Vector3 hitPos)
    {
        //총구 효과 
        muzzleFlashEffect.Play();
        // 탄피 배충 효과 재생 
        shellEjectEffect.Play();

        audioSource.PlayOneShot(gunData.shotClip);

        bulletLineRenderer.SetPosition(0, fireTransform.position);
        bulletLineRenderer.SetPosition(1,hitPos);
        bulletLineRenderer.enabled = true;

        yield return wait;

        //라인 렌더러 비활성화
        bulletLineRenderer.enabled = false;
    }

    //재장전 시도
    public bool Reload()
    {
        if(state==State.Reloading||ammoRemain<=0||magAmmo>=gunData.magCapacity)
        {
            return false;
        }

        //재장전 처리 시작
        StartCoroutine(ReloadRoutine());

        return true;
    }
    IEnumerator ReloadRoutine()
    {
        state = State.Reloading;
        //재장전 소리 재생
       audioSource.PlayOneShot(gunData.reloadClip);

        //재장전 소요 시간만큼 기다림

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
