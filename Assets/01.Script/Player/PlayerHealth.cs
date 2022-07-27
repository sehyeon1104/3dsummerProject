using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : LivingEntity
{
    [SerializeField]
    Slider healthSlider;  //체력을 표시할 슬라이더
    [SerializeField]
     AudioClip deathClip; // 사망 소리
    [SerializeField]      
    AudioClip hitClip;    //피격 소리
    [SerializeField]
    AudioClip itemPickupClip; // 아이템 습득 소리

    AudioSource playerAudio; //플레이어 소리 재생기
    Animator playerAnimator; // 플레이어 애니메이터
     
    PlayerMovement playerMovement; //플레이어 움직임 컴포넌트
    PlayerShooter playerShooter;   //플레이어 슈터 컴포넌트
    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerAudio =GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();
    }
    public override void Die()
    {
        //부모의 Die 실행
        base.Die();

        // 체력 슬라이더 비활성화
        healthSlider.gameObject.SetActive(false);

        //사망을 재생
        playerAudio.PlayOneShot(deathClip);

        //Die 트리거 발생
        playerAnimator.SetTrigger("Die");

        //플레이어 소식을 받는 컴포넌트 비활성화
        playerMovement.enabled = false;
        playerShooter.enabled = false;
    }
    protected override void OnEnable()
    {
        //부모(LivingEntity)의  OnEnable 실행
        base.OnEnable();

        //체려바 (슬라이더)
        healthSlider.gameObject.SetActive(true);
        healthSlider.maxValue = startingHealth;
        healthSlider.value = health;

        //플레이어 조작을 받는 컴포넌트 활성화
        playerMovement.enabled = true;
        playerShooter.enabled = true;
    }
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if(!dead)
        {
            playerAudio.PlayOneShot(hitClip);
        }
        //부모의 onDamge실행
        base.OnDamage(damage, hitPoint, hitNormal);

        // 갱신된 체력을 슬라이더에 반영
        healthSlider.value = health;
    }
    public override void RestoreHealth(float newHealth)
    {
        //부모의 RestoreHealth
        base.RestoreHealth(newHealth);

        healthSlider.value = health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!dead)
        {
            //충돌한 상대방으로부터  Item 컴포넌트 가져오기
            IItem item = other.GetComponent<IItem>(); 
             //item 가져오기 성공
            if(item != null)
            {
                item.Use(gameObject);

                playerAudio.PlayOneShot(itemPickupClip);
            }
        }
    }
}
