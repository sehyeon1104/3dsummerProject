using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="SO/ZombieData",fileName ="Zombie Data")]
public class ZombieData : ScriptableObject
{
    public float health = 100f;              // ü��
    public float damage = 20f;               // ���ݷ�
    public float speed = 2f;                 // �̵��ӵ�
    public float timeAttack = 0.5f;          // ���ݼӵ�
    public int score = 100;               // ȹ������
    public Color skinColor = Color.white;    // ���� ��Ų ��
}
