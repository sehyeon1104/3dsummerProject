using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // �׺�޽� ���� �ڵ�
public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items; //Prefab
    public Transform playerTransform; // �÷��̾��� transform

    public float maxDistance = 5f; //�÷��̾� ��ġ���� �������� ��ġ�� �ִ� �ݰ�

    public float timeBetSpawnMax = 7f; //�ִ� �ð� ����
    public float timeBetSpawnMin = 2f; //�ּ� �ð� ����
    float timeBetSpawn; //���� ����
    float lastSpawnTime; //������ ���� ����
    void Start()
    {
        // ���� ���ݰ� ������ ���� ���� �ʱ�ȭ 
      timeBetSpawn=Random.Range(timeBetSpawnMin,timeBetSpawnMax);
        lastSpawnTime = 0;
    }
    void Update()
    {
        // ���� ������ ������ ���� �������� �����ֱ� �̻� ������ ��,
        if (Time.time >= lastSpawnTime + timeBetSpawn && playerTransform != null)
        {
            //������ ���� �ð� �ʱ�ȭ
            lastSpawnTime = Time.time;
            //���� �ֱ� �������� ����
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            //������ ���� ����
            Spawn();
        }
    }

    private void Spawn()
    {
        //�÷��̾� ��ó�� ���񿡼� ���� ���� ��ġ ��������
        Vector3 spawnPosition = GetRandomPointOnNavMesh(playerTransform.position, maxDistance);

        //�ٴڿ��� 0.5��ŭ �ø���
        spawnPosition += Vector3.up * 0.5f;

        //������ �� �ϳ��� �������� ��� ���� ��ġ�� ����

        GameObject selectedItem = items[Random.Range(0, items.Length)];

        GameObject item = Instantiate(selectedItem, spawnPosition, Quaternion.identity);

        Destroy(item, 5f);
    }
    // �׺�Ž� ���� ������ ��ġ�� ��ȯ�ϴ� �޼���
    //  center�� �߽����� distance �ݰ� �ȿ����� ������ ��ġ�� ã��
    Vector3 GetRandomPointOnNavMesh(Vector3 center,float distance)
    {
        //center�� �߽����� �������� distane �� ���ȿ��� ������ ��ġ�ϳ��� ����
        //Random.insideUnitSphere�� �������� 1�� ������ ������ �� ���� ��ȯ�ϴ� �޼���
        Vector3 randomPos = Random.insideUnitSphere * distance + center;

        NavMeshHit hit;
        //distance�ȿ��� randompos�� ���� ����� ����Ž� ���� ������ ã��
        //navmesh.allareas�� navmesh�������� ��� ����
        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);

        return hit.position;
    }
}
