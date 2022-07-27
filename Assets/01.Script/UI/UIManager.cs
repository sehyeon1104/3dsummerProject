using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
   static  UIManager instance;

    static public UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    public Text ammoText;           //ammo
    public Text scoreText;          //score
    public Text waveText;           //wave
    public GameObject gameOverUI;   // if GameOver

    //탄알 표시 텍스트 갱신
    public void UpdateAmmoText(int magAmmo, int reaminAmmo)
    {
        ammoText.text = $"{magAmmo} / {reaminAmmo}";
    }
    public void UpdateScoreText(int newScore)
    {
        scoreText.text = $"Score : {newScore}";
    }

    public void UpdateWaveText(int wave,int count )
    {
        waveText.text = $"Wave : {wave} \nEnemy : {count}";
    }

    //gameover enabled

    public void SetActiveGameOverUI(bool active)
    {
        gameOverUI.SetActive(active);
    }

    //restart

    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
