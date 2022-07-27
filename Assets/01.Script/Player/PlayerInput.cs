using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerInput : MonoBehaviour
{
    public string moveAxisName = "Vertical"; //앞 뒤 움직임 
    public string rotateAxisName = "Horizontal"; // 좌 우 움직임
    public string fireButtonName = "Fire1"; // 발사 버튼
    public string reloadButtonName = "Reload";

    public  float move { get; private set; }
    public float rotate { get; private set; }
    public bool fire { get; private set; }
    public bool reload { get; private set; }
    
    void Update()
    {
        // gameover

        // move 입력 처리
        move=Input.GetAxis(moveAxisName);
        //rotate 입력
        rotate = Input.GetAxis(rotateAxisName);
        // fire 입력
        fire =Input.GetButtonDown(fireButtonName);
        //reload 입력
        reload =Input.GetButtonDown(reloadButtonName);
    }
}
