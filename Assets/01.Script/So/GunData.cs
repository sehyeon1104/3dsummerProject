using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/GunData",fileName ="Gun Data")]
public class GunData : ScriptableObject
{
    public AudioClip shotClip; //�߻� �Ҹ�
    public AudioClip reloadClip; //���� �Ҹ�

    public float damage = 25f; // ���ݷ�

    public int startAmmoReamain = 100; //ó���� �־��� ��ü ź�� 
    public int magCapacity = 25; // �Ѿ� �뷮

    public float timeBetFire = 0.12f; // ź�� �߻� ����
    public float reloadTime = 1.8f; // ���� �ð�
}
