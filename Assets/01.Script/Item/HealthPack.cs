using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour,IItem
{
    //ü���� ȸ���� ��ġ
    public float health=50;
    public void Use(GameObject target)
    {
        LivingEntity life = target.GetComponent<LivingEntity>();

        if(life != null)
        {
            //ü�� ȸ�� ����
            life.RestoreHealth(health);
        }
        Destroy(gameObject);
    }

 
}
