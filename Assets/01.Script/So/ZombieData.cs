using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Scritable/ZombieData",fileName ="Zombie Data")]
public class ZombieData : ScriptableObject
{
    public float health = 100f;
    public float damage = 20f;
    public float speed = 2f;
    public float timeAttack = 0.5f;
    public float score = 100f;
    public Color skinColor = Color.white;
}
