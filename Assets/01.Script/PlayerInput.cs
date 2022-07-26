using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerInput : MonoBehaviour
{
    public string moveAxisName = "Vertical"; //�� �� ������ 
    public string rotateAxisName = "Horizontal"; // �� �� ������
    public string fireButtonName = "Fire1"; // �߻� ��ư
    public string reloadButtonName = "Reload";

    public  float move { get; private set; }
    public float rotate { get; private set; }
    public bool fire { get; private set; }
    public bool reload { get; private set; }
    
    void Update()
    {
        // gameover

        // move �Է� ó��
        move=Input.GetAxis(moveAxisName);
        //rotate �Է�
        rotate = Input.GetAxis(rotateAxisName);
        // fire �Է�
        fire =Input.GetButtonDown(fireButtonName);
        //reload �Է�
        reload =Input.GetButtonDown(reloadButtonName);
    }
}
