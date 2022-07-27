using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour,IItem
{
    public int ammo = 30;
    public void Use(GameObject target)
    {
        //playerShooter ������Ʈ ��������
        PlayerShooter playerShooter =target.GetComponent<PlayerShooter>();

        if(playerShooter!=null&&playerShooter.gun!=null)
        {
            playerShooter.gun.ammoRemain += ammo;
        }
        //���Ǿ����Ƿ� �ڱ��ڽ��� �ı�
        Destroy(gameObject);
    }

}
