using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : LivingEntity
{
    public ParticleSystem hitEffect; // �ǰ� �� ����� ��ƼŬ ȿ��
    public AudioClip deathSound;     // ��� �� ����� �Ҹ�
    public AudioClip hitSound;       // �ǰ� �� ����� �Ҹ�
    public ZombieData zombieData;    // ����Ʈ ���� ������

    Animator zombieAnimator;       // �ִϸ��̼� ������Ʈ
    AudioSource zombieAudioPlayer; // ����� �ҽ� ������Ʈ
     Renderer zombieRenderer;      // ������ ������Ʈ 

    
    private float damage = 20f;             // ���ݷ� 
    private float timeBetAttack = 0.5f;     // ���� ����
    private float lastAttackTime;           // ������ ���� ����
    float score = 100;                        // ȹ�� ����
    private void Awake()                   
    {
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
    }
    // Update is called once per frame
    void Update()
    {
        

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
