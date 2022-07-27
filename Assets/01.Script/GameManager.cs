using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance=FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    int score = 0; //현재 게임 점수
    public bool isGameOver { get; private set; }
    private void Awake()
    {
        //현재 싱글턴 오브젝트가 된 다른  GameManagerObject가 있다면 
        if(instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        // 플레이어 캐릭터의 사망 이벤트 발생시 게임오버 호출
        FindObjectOfType<PlayerHealth>().onDeath += EndGame;
    }

    public void EndGame()
    {
        isGameOver = true;

        //게임 오버 UI 활성화
        UIManager.Instance.SetActiveGameOverUI(true);
    }

    public void Addscore(int newScore)
    {
        if(!isGameOver)
        {
            score += newScore;

            //점수 UI 갱신
            UIManager.Instance.UpdateScoreText(score);
        }
    }
}
