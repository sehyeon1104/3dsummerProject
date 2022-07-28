using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour,IDamagable
{
    public float startingHealth = 100f; //���� ü��
    public float health { get;protected set; } //���� ü��
    public bool dead { get; protected set; } // ��� ����
    public event Action onDeath; // ����� �ߵ��� �̺�Ʈ

protected virtual void OnEnable()
    {
        dead = false;
        health = startingHealth;
    }
    //������ �Դ� ���
    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        //��������ŭ ü�� ����
        health -= damage;

        //ü���� 0���� && ���� ���� �ʾҴٸ� ���
        if(health <= 0&&!dead)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        if(onDeath!=null)
        {
            onDeath();
        }
        dead = true;
    }

    //ü���� ȸ���ϴ� ���
    public virtual void RestoreHealth(float newHealth)
    {
        if (dead)
            return;

        health += newHealth;
    }    
}
