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
    float timeVetSpawn;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
