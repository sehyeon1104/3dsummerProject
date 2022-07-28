using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Zombie : LivingEntity
{
    public ParticleSystem hitEffect; // �ǰ� �� ����� ��ƼŬ ȿ��
    public AudioClip deathSound;     // ��� �� ����� �Ҹ�
    public AudioClip hitSound;       // �ǰ� �� ����� �Ҹ�
    public ZombieData zombieData;    // ����Ʈ ���� ������

    public LayerMask whatIsTarget; //���� ��� 

    LivingEntity targetEntity; // ���� ���
    NavMeshAgent navMeshAgent; // ��� ����
    
    Animator zombieAnimator;       // �ִϸ��̼� ������Ʈ
    AudioSource zombieAudioPlayer; // ����� �ҽ� ������Ʈ
     Renderer zombieRenderer;      // ������ ������Ʈ 
    
    private float damage = 20f;             // ���ݷ� 
    private float timeBetAttack = 0.5f;     // ���� ����
    private float lastAttackTime;           // ������ ���� ����
    float score = 100;                        // ȹ�� ����

    //���� ����� �����ϴ��� �˷��ִ� ������Ƽ

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
        //ü�� ����
        startingHealth = data.health;
        health = startingHealth;

        //���ݷ� ����
        damage = data.damage;

        //���� ����
        timeBetAttack = data.timeAttack;

        //ȹ�� ����
        score = data.score;

        //���� ��Ų �÷�
        zombieRenderer.material.color = data.skinColor;

        // �׺񿡼� ������Ʈ�� �̵� �ӵ� ����
        navMeshAgent.speed = data.speed;
    }
    private void Start()
    {
        // ���� ������Ʈ Ȱ��ȭ ���ÿ�
        StartCoroutine(UpdatePath());
    }
    void Update()
    {
        //���� ����� ���� ���ο� ���� �ٸ� �ִϸ��̼� ���
        zombieAnimator.SetBool("HasTarget", hasTarget);
    }
    IEnumerator UpdatePath()
    {
        while (!dead)
        {
            if(hasTarget)
            {
                //���� ��� ���� : ��θ� �����ϰ� AI �̵��� ��� ����
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetEntity.transform.position);
            }
            else
            {
                //���� ��� ���� : AI �̵� ����
                navMeshAgent.isStopped = true;

                //20 ������ �������� ���� ������ ���� �׷��� �� ���� ��ġ�� ��� �ݶ��̴��� ������
                //whatIsTarget ���̾ ���� �ݶ��̴��� ���������� �Ѵ�
                Collider[] colldiers = Physics.OverlapSphere(transform.position, 20f, whatIsTarget);  
                
                // ��� �ݶ��̴��� ��ȸ�ϸ� ��� �ִ� livingEntity�� ã��
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
            // �ǰ� ��ƼŬ ���
            hitEffect.transform.position= hitPoint; 
            hitEffect.transform.rotation =Quaternion.LookRotation(hitNormal);
            hitEffect.Play();

            //�ǰ� ȿ���� ���
            zombieAudioPlayer.PlayOneShot(hitSound);
        }

        // �θ��� OnDamage ����
        base.OnDamage(damage, hitPoint, hitNormal);
    }
    public override void Die()
    {
        //�θ��� Die ����
        base.Die();
        //�ڽ��� ��� �ݶ��̴��� ��Ȱ��ȭ
        Collider[] zombieColliders = GetComponents<Collider>();
        foreach(Collider c in zombieColliders)
        {
            c.enabled = false;
        }

        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;
        //��� �ִϸ��̼� ���
        zombieAnimator.SetTrigger("Die");

        //��� ȿ���� ���
        zombieAudioPlayer.PlayOneShot(deathSound);
    }
    private void OnTriggerStay(Collider other)
    {
        if(!dead && Time.time >= lastAttackTime+timeBetAttack)
        {
            LivingEntity attackTarget = other.GetComponent<LivingEntity>();
           if(attackTarget!=null)   //�÷��̾� ���� Ȯ��
            {

                //�ֱ� ���� �ð� ����
                lastAttackTime =Time.time;

                //������ �ǰ� ��ġ�� �ǰ� ���� �ٻ簪���� ���
                Vector3 hitPoint = other.ClosestPoint(transform.position);

                Vector3 hitNormal = transform.position - other.transform.position;

                attackTarget.OnDamage(damage, hitPoint, hitNormal); 
            }
        }

    }

}
