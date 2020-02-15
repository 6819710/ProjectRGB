using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float inputDirection; // X Move
    private float verticalVelocity; // Y Move

    private CharacterController controller;
    private Vector3 moveVector;

    private Animator anim;
    private Animator bodyAnim;

    // Speeds and Feeds
    public float gravity;
    public float speed;
    public float jump;

    // Flags
    public bool moveWhileJump;

    private bool isLeft;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        bodyAnim = GameObject.Find("PlayerBody").GetComponent<Animator>();
        isLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        float previousX = 0;
        if (transform.position.z == 0)
            previousX = transform.position.x;

        UpdateDirection();

        if (controller.isGrounded)
        {
            verticalVelocity = 0;
            if (Input.GetAxis("Jump") != 0.0f)
                verticalVelocity = jump;
        }
        else
            verticalVelocity -= gravity * Time.deltaTime;

        moveVector = new Vector3(inputDirection, verticalVelocity, 0);

        controller.Move(moveVector * Time.deltaTime);

        if (transform.position.z != 0)
        {
            Vector3 newPos = transform.position;
            newPos.x = previousX;
            newPos.z = 0;
            transform.position = newPos;
        }
    }

    void UpdateDirection()
    {
        if (moveWhileJump || controller.isGrounded)
        {
            inputDirection = Input.GetAxis("Horizontal") * speed;
            if (inputDirection < 0.0f && !isLeft)
            {
                Debug.Log("Turn Left");
                anim.SetTrigger("TurnL");
                anim.ResetTrigger("TurnR");
                isLeft = true;
            }
            else if (inputDirection > 0.0f && isLeft)
            {
                Debug.Log("Turn Right");
                anim.SetTrigger("TurnR");
                anim.ResetTrigger("TurnL");
                isLeft = false;
            }
        }

        if(controller.isGrounded && inputDirection != 0.0f)
        {
            bodyAnim.ResetTrigger("Idle");
            bodyAnim.SetTrigger("Walk");
        }
        else if(inputDirection == 0.0f)
        {
            bodyAnim.ResetTrigger("Walk");
            bodyAnim.SetTrigger("Idle");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "ClimbTrigger":
                if (verticalVelocity > 0)
                {
                    verticalVelocity = jump;
                    moveVector = new Vector3(inputDirection, verticalVelocity, 0);
                }
                break;
        }
    }
}
