using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="SO/ZombieData",fileName ="Zombie Data")]
public class ZombieData : ScriptableObject
{
    public float health = 100f;              // 체력
    public float damage = 20f;               // 공격력
    public float speed = 2f;                 // 이동속도
    public float timeAttack = 0.5f;          // 공격속도
    public int score = 100;               // 획득점수
    public Color skinColor = Color.white;    // 좀비 스킨 색
}
