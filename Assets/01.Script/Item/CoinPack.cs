using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPack : MonoBehaviour,IItem
{
    public int score = 200; //증가할 점수

    public void Use(GameObject target)
    {
        //게임 매니저에 점수 추가
       
        GameManager.Instance.Addscore(score);
        //사용되었으므로 자신을 파괴
        Destroy(gameObject);
    }
}
