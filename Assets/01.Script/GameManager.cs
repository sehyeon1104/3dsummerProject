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
    int score = 0; //���� ���� ����
    public bool isGameOver { get; private set; }
    private void Awake()
    {
        //���� �̱��� ������Ʈ�� �� �ٸ�  GameManagerObject�� �ִٸ� 
        if(instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        // �÷��̾� ĳ������ ��� �̺�Ʈ �߻��� ���ӿ��� ȣ��
        FindObjectOfType<PlayerHealth>().onDeath += EndGame;
    }

    public void EndGame()
    {
        isGameOver = true;

        //���� ���� UI Ȱ��ȭ
        UIManager.Instance.SetActiveGameOverUI(true);
    }

    public void Addscore(int newScore)
    {
        if(!isGameOver)
        {
            score += newScore;

            //���� UI ����
            UIManager.Instance.UpdateScoreText(score);
        }
    }
}
