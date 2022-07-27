using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour,IItem
{
    //체력을 회복할 수치
    public float health=50;
    public void Use(GameObject target)
    {
        LivingEntity life = target.GetComponent<LivingEntity>();

        if(life != null)
        {
            //체력 회복 실행
            life.RestoreHealth(health);
        }
        Destroy(gameObject);
    }

 
}
