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
    float timeBetSpawn; //생성 간격
    float lastSpawnTime; //마지막 생성 지점
    void Start()
    {
        // 생성 간격과 마지막 생성 시점 초기화 
      timeBetSpawn=Random.Range(timeBetSpawnMin,timeBetSpawnMax);
        lastSpawnTime = 0;
    }
    void Update()
    {
        // 현재 시점이 마지막 생성 시점에서 생성주기 이상 지났을 때,
        if (Time.time >= lastSpawnTime + timeBetSpawn && playerTransform != null)
        {
            //마지막 생성 시간 초기화
            lastSpawnTime = Time.time;
            //생성 주기 랜덤으로 변경
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            //아이템 생성 실행
            Spawn();
        }
    }

    private void Spawn()
    {
       //플레이어 근처에 내비에서 위의 랜덤 위치 가져오기


    }
    // 네비매시 위의 랜덤한 위치를 변환하는 메서드
    //  center를 중심으로 distance 반경 안에서의 랜덤한 위치를 찾음
    Vector3 GetRandomPointOnNavMesh(Vector3 center,float distance)
    {
        Vector3 randomPos = Random.insideUnitSphere * distance + center;

        NavMeshHit hit;
        //distance안에서 randompos에 가장 가까운 내비매시 위의 한점을 찾음
        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);

        return hit.position;
    }
}
