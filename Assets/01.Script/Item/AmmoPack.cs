using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour,IItem
{
    public int ammo = 30;
    public void Use(GameObject target)
    {
        //playerShooter 컴포넌트 가져오기
        PlayerShooter playerShooter =target.GetComponent<PlayerShooter>();

        if(playerShooter!=null&&playerShooter.gun!=null)
        {
            playerShooter.gun.ammoRemain += ammo;
        }
        //사용되었으므로 자기자신을 파괴
        Destroy(gameObject);
    }

}
