using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPack : MonoBehaviour,IItem
{
    public int score = 200; //������ ����

    public void Use(GameObject target)
    {
        //���� �Ŵ����� ���� �߰�
       
        GameManager.Instance.Addscore(score);
        //���Ǿ����Ƿ� �ڽ��� �ı�
        Destroy(gameObject);
    }
}
