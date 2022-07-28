using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/GunData",fileName ="Gun Data")]
public class GunData : ScriptableObject
{
    public AudioClip shotClip; //발사 소리
    public AudioClip reloadClip; //장전 소리

    public float damage = 25f; // 공격력

    public int startAmmoReamain = 100; //처음에 주어질 전체 탄알 
    public int magCapacity = 25; // 총알 용량

    public float timeBetFire = 0.12f; // 탄알 발사 간격
    public float reloadTime = 1.8f; // 장전 시간
}
