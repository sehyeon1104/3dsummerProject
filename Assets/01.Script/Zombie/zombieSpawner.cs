using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieSpawner : MonoBehaviour
{
    public Zombie zombiePrefab; // ������ ���� ���� ������

    public ZombieData[] zombieDatas; //����� ���� �¾� ������
    public Transform[] spawnPoints; // ���� ��ȯ�� ��ġ

    List<Zombie> zombies = new List<Zombie>();
    int wave;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���� ���� ������ ���� �������� ����
        if(GameManager.Instance!=null&&GameManager.Instance.isGameOver)
        {
            return;
        }

        //���� ��� ����ģ ��� �������� ����
        if(zombies.Count<=0)
        {
            SpawnWave();
        }
        UpdateUI();
    }
    void UpdateUI()
    {
        if(UIManager.Instance!=null)
        {
            UIManager.Instance.UpdateWaveText(wave, zombies.Count);
        }
    }
    void SpawnWave()
    {
        //���̺� 1����
        wave++;

        // ���� ���̺� * 1.5�� �ݿø� �� �� ��ŭ ���� ����   
        int spawnCount = Mathf.RoundToInt(wave * 1.5f);

        // spawnCount ��ŭ ���� ����
        for(int i = 0; i < spawnCount; i++)
        {
            // ���� ����
            CreatZombie();
        }
    }
    void CreatZombie()
    {
        // ����� ���� ������ �������� ����
        ZombieData zombiData = zombieDatas[Random.Range(0,zombieDatas.Length)];

        //������ ��ġ�� �������� ���� 
        Transform spawnpoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // ���� ���������κ��� ���� ����
       Zombie zombie =Instantiate(zombiePrefab,spawnpoint.position, spawnpoint.rotation);

        // ������ ������ �ɷ�ġ ����
        zombie.Setup(zombiData);

        //������ onDeath �̺�Ʈ�� �͸� �޼��� ���
        //����� ���� ����Ʈ���� ����
        zombie.onDeath += () => zombies.Remove(zombie);

        //����� ���� 10�ʵڿ� �ı�
        zombie.onDeath +=()=> Destroy(zombie.gameObject,10f );

        // ���� ����� ���� ���
        zombie.onDeath += () => GameManager.Instance.Addscore(zombiData.score);

        //������ ���� ����Ʈ�� �߰�
        zombies.Add(zombie);
    }
}
