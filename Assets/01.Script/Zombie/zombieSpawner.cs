using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieSpawner : MonoBehaviour
{
    public Zombie zombiePrefab; // 생성할 좀비 원본 프리펩

    public ZombieData[] zombieDatas; //사용할 좀비 셋업 데이터
    public Transform[] spawnPoints; // 좀비 소환할 위치

    List<Zombie> zombies = new List<Zombie>();
    int wave;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //게임 오버 상태일 때는 생성하지 않음
        if(GameManager.Instance!=null&&GameManager.Instance.isGameOver)
        {
            return;
        }

        //좀비를 모두 물리친 경우 다음스폰 실행
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
        //웨이브 1증가
        wave++;

        // 현재 웨이브 * 1.5를 반올림 한 수 만큼 좀비 생성   
        int spawnCount = Mathf.RoundToInt(wave * 1.5f);

        // spawnCount 만큼 좀비 생성
        for(int i = 0; i < spawnCount; i++)
        {
            // 좀비 생성
            CreatZombie();
        }
    }
    void CreatZombie()
    {
        // 사용할 좀비 데이터 랜덤으로 결정
        ZombieData zombiData = zombieDatas[Random.Range(0,zombieDatas.Length)];

        //생성할 위치를 랜덤으로 결정 
        Transform spawnpoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // 좀비 프리펩으로부터 좀비 생성
       Zombie zombie =Instantiate(zombiePrefab,spawnpoint.position, spawnpoint.rotation);

        // 생성한 좀비의 능력치 설정
        zombie.Setup(zombiData);

        //좀비의 onDeath 이벤트에 익명 메서드 등록
        //사망한 좀비를 리스트에서 제거
        zombie.onDeath += () => zombies.Remove(zombie);

        //사망한 좀비를 10초뒤에 파괴
        zombie.onDeath +=()=> Destroy(zombie.gameObject,10f );

        // 좀비 사망시 점수 상승
        zombie.onDeath += () => GameManager.Instance.Addscore(zombiData.score);

        //생성된 좀비를 리스트에 추가
        zombies.Add(zombie);
    }
}
