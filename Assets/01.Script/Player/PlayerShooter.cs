using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gun 오브젝트를 쏘거나 재장전 
// 양손이 총에 위치하도록 조정
public class PlayerShooter : MonoBehaviour
{
    public
     Gun gun; //사용할 총

    [SerializeField]
    Transform gunPivot; //총 배치 기준점 
    [SerializeField]
    Transform leftMount; //왼손이 위치할 지점
    [SerializeField]
    Transform rightMount; //오른손이 위치할 지점

    PlayerInput playerInput; // player input
    Animator playerAni; // player animator

    Vector3 gunPivotPos;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAni = GetComponent<Animator>();
        gunPivotPos = gunPivot.position;
    }

    private void OnEnable() // 활성화 될때 
    {
        gun.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        gun.gameObject.SetActive(false);
    }
    void Update()
    {
        gunPivot.position = gunPivotPos;
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
        UpdateUI();
    }

    void UpdateUI()
    {
        if(gun!=null&&UIManager.Instance!=null)
        {
            // UIManager의 탄알 표시
            UIManager.Instance.UpdateAmmoText(gun.magAmmo,gun.ammoRemain);
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        // 총의 기준점 gunPivot을 3D 모델의 오른쪽 팔꿈치 위치로
        gunPivotPos = playerAni.GetIKHintPosition(AvatarIKHint.RightElbow);
        gunPivotPos.x -= 0.1f;
        //IK를 사용하여 왼손의 위치와 회전을 총의 왼쪽 손잡이에 맞춤
        playerAni.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        playerAni.SetIKPosition(AvatarIKGoal.LeftHand, leftMount.position);

        playerAni.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
        playerAni.SetIKRotation(AvatarIKGoal.LeftHand, leftMount.rotation);

        //IK를 사용하여 오른손의 위치와 회전을 총의 왼쪽 손잡이에 맞춤
        playerAni.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        playerAni.SetIKPosition(AvatarIKGoal.RightHand, rightMount.position);

        playerAni.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        playerAni.SetIKRotation(AvatarIKGoal.RightHand, rightMount.rotation);
    }
}
