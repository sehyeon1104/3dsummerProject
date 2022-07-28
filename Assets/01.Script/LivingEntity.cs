using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour,IDamagable
{
    public float startingHealth = 100f; //시작 체력
    public float health { get;protected set; } //현재 체력
    public bool dead { get; protected set; } // 사망 상태
    public event Action onDeath; // 사망시 발동할 이벤트

protected virtual void OnEnable()
    {
        dead = false;
        health = startingHealth;
    }
    //데미지 입는 기능
    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        //데미지만큼 체력 감소
        health -= damage;

        //체력이 0이하 && 아직 죽지 않았다면 사망
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

    //체력을 회복하는 기능
    public virtual void RestoreHealth(float newHealth)
    {
        if (dead)
            return;

        health += newHealth;
    }    
}
