using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // 네비메쉬 관련 코드
public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items; //Prefab
    public Transform playerTransform; // 플레이어의 transform

    public float maxDistance = 5f; //플레이어 위치에서 아이템이 배치될 최대 반경

    public float timeBetSpawnMax = 7f; //최대 시간 간격
    public float timeBetSpawnMin = 2f; //최소 시간 간격
    float timeVetSpawn;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
