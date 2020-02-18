using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static class Anim
    {
        // Body Animations
        public static string Walking = "Walk";
        public static string Idle = "Idle";
        public static string Goggles = "Goggle";

        // Rot Animations
        public static string TurnLeft = "TurnL";
        public static string TurnRight = "TurnR";
    }

    private enum AnimState
    {
        Walking,
        Netural,
        Idle,
        Goggles,

        TurnLeft,
        TurnRight
    }

    private enum GoggleState
    {
        White,
        Green,
        Red,
        Blue
    }

    // Movement Setting
    [Header("Movement Settings")]
    public float _gravity;
    public float _speed;
    public float _jumpPower;
    public bool _moveWhileJump;
    public bool _idleJump;

    // Movement Variables
    private float inputDirection;
    private float verticalVelocity;

    private CharacterController controller;
    private Vector3 movementVector;

    private Animator movementAnim;
    private Animator rotAnim;
    private AnimState movementState;
    private AnimState rotState;

    // Button Locking
    public bool jumpPressed;
    public bool greenPressed;
    public bool redPressed;
    public bool bluePressed;

    // Goggle Variables
    private GoggleState goggleState;
    private bool goggleChange;

    // Start is called before the first frame update
    void Start()
    {
        // Setup Controller
        controller = GetComponent<CharacterController>();
        // Setup Animators
        movementAnim = GameObject.Find("PlayerModel").GetComponent<Animator>();
        rotAnim = GetComponent<Animator>();
        movementState = AnimState.Netural;
        rotState = AnimState.TurnRight;
        goggleState = GoggleState.White;
        goggleChange = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get Data for Z Knockoff Rollback
        float previousX = 0;
        if (transform.position.z == 0)
            previousX = transform.position.x;

        // Read Inputs
        if (goggleChange && controller.isGrounded)
            inputDirection = 0.0f;
        else if(!goggleChange && _moveWhileJump)
            inputDirection = Input.GetAxis("Horizontal") * _speed;

        UpdateMovementAnim();

        // Process Jump
        if (controller.isGrounded)
        {
            verticalVelocity = 0.0f;
            if (Input.GetAxis("Jump") != 0.0f && !jumpPressed)
            {
                verticalVelocity = _jumpPower;
                jumpPressed = true;
            }
        }
        else
            verticalVelocity -= _gravity * Time.deltaTime;

        // Move Character
            movementVector = new Vector3(inputDirection, verticalVelocity, 0);
        
        controller.Move(movementVector * Time.deltaTime);

        // Correct if Z Knockoff
        if(transform.position.z != 0.0f)
        {
            Vector3 pos = transform.position;
            pos.x = previousX;
            pos.z = 0;
            transform.position = pos;
        }

        // Process Goggles
        if (Input.GetAxis("Green") != 0.0f && !greenPressed)
        {
            greenPressed = true;
            goggleChange = true;
        }

        // Reset Inputs
        if (Input.GetAxis("Jump") == 0.0f)
            jumpPressed = false;
        if (Input.GetAxis("Green") == 0.0f)
            greenPressed = false;
        if (Input.GetAxis("Red") == 0.0f)
            redPressed = false;
        if (Input.GetAxis("Blue") == 0.0f)
            bluePressed = false;
    }

    void UpdateMovementAnim()
    {
        if(!goggleChange)
        {
            // Turning Animations
            if (_moveWhileJump || controller.isGrounded)
            {
                if (inputDirection < 0.0f)
                    SetAnimLeft();
                else if (inputDirection > 0.0f)
                    SetAnimRight();
            }

            // Walking Animations
            if ((controller.isGrounded || !_idleJump) && inputDirection != 0.0f)
                SetAnimWalking();
            // Idle Animations
            else
                SetAnimIdle();
        }
        else
        {
            SetAnimGoggles();
        }
    }

    void SetAnimWalking()
    {
        Debug.Log("SetWalking");
        if (movementState != AnimState.Walking)
        {
            Debug.Log("Walking");
            movementState = AnimState.Walking;
            movementAnim.SetBool(Anim.Idle, false);
            movementAnim.SetBool(Anim.Goggles, false);
            movementAnim.SetBool(Anim.Walking, true);
        }
    }

    void SetAnimIdle()
    {
        if (movementState != AnimState.Idle)
        {
            movementState = AnimState.Idle;
            movementAnim.SetBool(Anim.Walking, false);
            movementAnim.SetBool(Anim.Goggles, false);
            movementAnim.SetBool(Anim.Idle, true);
        }
    }

    void SetAnimGoggles()
    {
        if (movementState != AnimState.Goggles)
        {
            movementState = AnimState.Goggles;
            movementAnim.SetBool(Anim.Walking, false);
            movementAnim.SetBool(Anim.Idle, false);
            movementAnim.SetBool(Anim.Goggles, true);
        }
        else if (movementAnim.GetBool(Anim.Goggles) == false)
            goggleChange = false;
    }

    void SetAnimLeft()
    {
        if (rotState != AnimState.TurnLeft)
        {
            rotState = AnimState.TurnLeft;
            rotAnim.ResetTrigger(Anim.TurnRight);
            rotAnim.SetTrigger(Anim.TurnLeft);
        }
    }

    void SetAnimRight()
    {
        if (rotState != AnimState.TurnRight)
        {
            rotState = AnimState.TurnRight;
            rotAnim.ResetTrigger(Anim.TurnLeft);
            rotAnim.SetTrigger(Anim.TurnRight);
        }
    }
}
