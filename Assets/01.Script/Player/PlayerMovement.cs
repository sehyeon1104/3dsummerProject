using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
     float moveSpeed = 5f;
    [SerializeField]
    float rotateSpeed = 180f;

     PlayerInput playerinput;
    Rigidbody playerRigidbody;
    Animator animator;
    void Start()
    {
        //Ä³½Ì
        playerinput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rotate();
        Move();
        Ani();
    }
    void Rotate()
    {
         float turn =playerinput.rotate *rotateSpeed*Time.deltaTime;

        playerRigidbody.rotation *=Quaternion.Euler(0,turn,0);  
    }

    void Move()
    {
        Vector3 moveDistance =playerinput.move*transform.forward*moveSpeed*Time.deltaTime;

        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
    }
    void Ani()
    {
        animator.SetFloat("Move", playerinput.move);
    }
}
