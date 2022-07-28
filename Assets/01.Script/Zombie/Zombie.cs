using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Zombie : LivingEntity
{
    public ParticleSystem hitEffect; // 피격 시 재생할 파티클 효과
    public AudioClip deathSound;     // 사망 시 재샐할 소리
    public AudioClip hitSound;       // 피격 시 재생할 소리
    public ZombieData zombieData;    // 디폴트 좀비 데이터

    public LayerMask whatIsTarget; //추적 대상 

    LivingEntity targetEntity; // 추적 대상
    NavMeshAgent navMeshAgent; // 경로 지정
    
    Animator zombieAnimator;       // 애니메이션 컴포넌트
    AudioSource zombieAudioPlayer; // 오디오 소스 컴포넌트
     Renderer zombieRenderer;      // 렌더러 컴포넌트 
    
    private float damage = 20f;             // 공격력 
    private float timeBetAttack = 0.5f;     // 공격 간격
    private float lastAttackTime;           // 마지막 공격 시점
    float score = 100;                        // 획득 점수

    //추적 대상이 존재하는지 알려주는 프로퍼티

    bool hasTarget
    {
        get
        {
            if(targetEntity != null&&!targetEntity.dead)
            {

                return true;
            }
            return false;
        }
    }
    private void Awake()                   
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
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

        // 네비에서 에이전트의 이동 속도 설정
        navMeshAgent.speed = data.speed;
    }
    private void Start()
    {
        // 게임 오브젝트 활성화 동시에
        StartCoroutine(UpdatePath());
    }
    void Update()
    {
        //추적 대상의 존재 여부에 따라 다른 애니메이션 재생
        zombieAnimator.SetBool("HasTarget", hasTarget);
    }
    IEnumerator UpdatePath()
    {
        while (!dead)
        {
            if(hasTarget)
            {
                //추적 대상 존재 : 경로를 갱신하고 AI 이동을 계속 진행
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetEntity.transform.position);
            }
            else
            {
                //추적 대상 없음 : AI 이동 중지
                navMeshAgent.isStopped = true;

                //20 유닛의 반지름을 가진 가상의 구를 그렸을 때 구와 겹치는 모든 콜라이더를 가져옴
                //whatIsTarget 레이어를 가진 콜라이더만 가져오도록 한다
                Collider[] colldiers = Physics.OverlapSphere(transform.position, 20f, whatIsTarget);  
                
                // 모든 콜라이더를 순회하면 살아 있는 livingEntity를 찾기
             for(int i = 0; i < colldiers.Length; i++)
                {
                    LivingEntity livingEntity = colldiers[i].GetComponent<LivingEntity>();

                    if(livingEntity != null&&!livingEntity.dead)
                    {
                        targetEntity = livingEntity;

                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
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

        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;
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
