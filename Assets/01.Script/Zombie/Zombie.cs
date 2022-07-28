using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : LivingEntity
{
    public ParticleSystem hitEffect; // 피격 시 재생할 파티클 효과
    public AudioClip deathSound;     // 사망 시 재샐할 소리
    public AudioClip hitSound;       // 피격 시 재생할 소리
    public ZombieData zombieData;    // 디폴트 좀비 데이터

    Animator zombieAnimator;       // 애니메이션 컴포넌트
    AudioSource zombieAudioPlayer; // 오디오 소스 컴포넌트
     Renderer zombieRenderer;      // 렌더러 컴포넌트 

    
    private float damage = 20f;             // 공격력 
    private float timeBetAttack = 0.5f;     // 공격 간격
    private float lastAttackTime;           // 마지막 공격 시점
    float score = 100;                        // 획득 점수
    private void Awake()                   
    {
        zombieAnimator = GetComponent<Animator>();
        zombieAudioPlayer = GetComponent<AudioSource>();

       zombieRenderer =GetComponentInChildren<Renderer>();

        Setup(zombieData);
    }

    public void Setup(ZombieData data)
    {
        //체력 설정
        startingHealth = data.health;
        health = startingHealth;

        //공격력 설정
        damage = data.damage;

        //공격 간격
        timeBetAttack = data.timeAttack;

        //획득 점수
        score = data.score;

        //좀비 스킨 컬러
        zombieRenderer.material.color = data.skinColor;
    }
    // Update is called once per frame
    void Update()
    {
        

    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if(!dead)
        {
            // 피격 파티클 출력
            hitEffect.transform.position= hitPoint; 
            hitEffect.transform.rotation =Quaternion.LookRotation(hitNormal);
            hitEffect.Play();

            //피격 효과음 출력
            zombieAudioPlayer.PlayOneShot(hitSound);
        }

        // 부모의 OnDamage 실행
        base.OnDamage(damage, hitPoint, hitNormal);
    }
    public override void Die()
    {
        //부모의 Die 실행
        base.Die();
        //자신의 모든 콜라이더를 비활성화
        Collider[] zombieColliders = GetComponents<Collider>();
        foreach(Collider c in zombieColliders)
        {
            c.enabled = false;
        }

        //사망 애니메이션 재셍
        zombieAnimator.SetTrigger("Die");

        //사망 효과음 재생
        zombieAudioPlayer.PlayOneShot(deathSound);
    }
    private void OnTriggerStay(Collider other)
    {
        if(!dead && Time.time >= lastAttackTime+timeBetAttack)
        {
            LivingEntity attackTarget = other.GetComponent<LivingEntity>();
           if(attackTarget!=null)   //플레이어 인지 확인
            {
                //최근 공격 시간 갱신
                lastAttackTime =Time.time;

                //상대방의 피격 위치와 피격 방향 근사값으로 계산
                Vector3 hitPoint = other.ClosestPoint(transform.position);

                Vector3 hitNormal = transform.position - other.transform.position;

                attackTarget.OnDamage(damage, hitPoint, hitNormal); 
            }
        }

    }

}
