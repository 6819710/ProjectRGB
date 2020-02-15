using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables
    public float inputDirection;
    public float vertialVelocity;

    public CharacterController controller;
    private Vector3 moveVector;

    private Animator movementAnim;
    private Animator rotAnim;

    // Speeds and Feeds
    public float _gravity;
    public float _speed;
    public float _jumpPower;

    // Flags
    public bool _moveWhileJump;
    private bool isLeft;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rotAnim = GetComponent<Animator>();
        movementAnim = GameObject.Find("PlayerModel").GetComponent<Animator>();
        isLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get data for knocked off rollback
        float previousX = 0;
        if (transform.position.z == 0)
            previousX = transform.position.x;

        UpdateAnimations();

        // Reset Gravity if Grounded
        if (controller.isGrounded)
        {
            vertialVelocity = 0;
            // Begin Jump if Called
            if (Input.GetAxis("Jump") != 0.0f)
                vertialVelocity = _jumpPower;
        }
        else
            vertialVelocity -= _gravity * Time.deltaTime;

        // Move Character
        moveVector = new Vector3(inputDirection, vertialVelocity, 0);
        controller.Move(moveVector * Time.deltaTime);

        // Correct if knocked off
        if (transform.position.z != 0)
        {
            Vector3 pos = transform.position;
            pos.x = previousX;
            pos.z = 0;
            transform.position = pos;
        }
    }

    /// <summary>
    /// Used for calling updates & transitions to character animations.
    /// </summary>
    void UpdateAnimations()
    {
        if (_moveWhileJump || controller.isGrounded)
        {
            // Get updated inputs
            inputDirection = Input.GetAxis("Horizontal") * _speed;
            
            // Turning Animations
            if (inputDirection < 0.0f && !isLeft)
            {
                rotAnim.ResetTrigger("TurnR");
                rotAnim.SetTrigger("TurnL");
                isLeft = true;
            }
            else if (inputDirection > 0.0f && isLeft)
            {
                rotAnim.ResetTrigger("TurnL");
                rotAnim.SetTrigger("TurnR");
                isLeft = false;
            }
        }

        if (controller.isGrounded && inputDirection != 0.0f)
        {
            // Trigger WalkAnimation
            movementAnim.ResetTrigger("Idle");
            movementAnim.SetTrigger("Walk");
        }
        else if (inputDirection == 0.0f)
        {
            // Trigger Idle Animation
            movementAnim.ResetTrigger("Walk");
            movementAnim.SetTrigger("Idle");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "ClimbTrigger":
                Debug.Log("ClimbTrigger");
                if(vertialVelocity > 0.0f)
                {
                    vertialVelocity = _jumpPower;
                    moveVector = new Vector3(inputDirection, vertialVelocity, 0);
                }
                break;
            case "BreakawayTrigger":
                Debug.Log("BreakawayTrigger");
                GameObject[] breakaway = GameObject.FindGameObjectsWithTag("Breakaway");
                foreach(GameObject o in breakaway)
                {
                    o.GetComponent<BoxCollider>().enabled = false;
                    o.GetComponent<MeshRenderer>().enabled = false;
                }
                break;
        }
    }
}
