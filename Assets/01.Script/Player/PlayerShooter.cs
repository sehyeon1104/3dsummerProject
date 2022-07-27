using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gun ������Ʈ�� ��ų� ������ 
// ����� �ѿ� ��ġ�ϵ��� ����
public class PlayerShooter : MonoBehaviour
{
    [SerializeField]
     Gun gun; //����� ��

    [SerializeField]
    Transform gunPivot; //�� ��ġ ������ 
    [SerializeField]
    Transform leftMount; //�޼��� ��ġ�� ����
    [SerializeField]
    Transform rightMount; //�������� ��ġ�� ����

    PlayerInput playerInput; // player input
    Animator playerAni; // player animator
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAni = GetComponent<Animator>();
    }

    private void OnEnable() // Ȱ��ȭ �ɶ� 
    {
        gun.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        gun.gameObject.SetActive(false);
    }
    void Update()
    {
        if (playerInput.fire)
        {
            gun.Fire();
        }
        else if(playerInput.reload)
        {
            if(gun.Reload())
            {
                playerAni.SetTrigger("Reload");
            }
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        // ���� ������ gunPivot�� 3D ���� ������ �Ȳ�ġ ��ġ��
        gunPivot.position = playerAni.GetIKHintPosition(AvatarIKHint.RightElbow);

        //IK�� ����Ͽ� �޼��� ��ġ�� ȸ���� ���� ���� �����̿� ����
        playerAni.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        playerAni.SetIKPosition(AvatarIKGoal.LeftHand, leftMount.position);

        playerAni.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
        playerAni.SetIKRotation(AvatarIKGoal.LeftHand, leftMount.rotation);

        //IK�� ����Ͽ� �������� ��ġ�� ȸ���� ���� ���� �����̿� ����
        playerAni.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        playerAni.SetIKPosition(AvatarIKGoal.RightHand, rightMount.position);

        playerAni.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        playerAni.SetIKRotation(AvatarIKGoal.RightHand, rightMount.rotation);
    }
}
