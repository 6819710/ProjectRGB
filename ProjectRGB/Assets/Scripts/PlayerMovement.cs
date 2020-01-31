using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float inputDirection; // X Move
    private float verticalVelocity; // Y Move
    private CharacterController controller;
    private Vector3 moveVector;

    // Speeds and Feeds
    public float gravity;
    public float speed;
    public float jump;

    // Flags
    public bool move_while_jump;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (move_while_jump)
            inputDirection = Input.GetAxis("Horizontal") * speed;
        if (controller.isGrounded)
        {
            verticalVelocity = 0;
            inputDirection = Input.GetAxis("Horizontal") * speed;
            if (Input.GetAxis("Jump") != 0.0f)
                verticalVelocity = jump;
        }
        else
            verticalVelocity -= gravity;
            

        moveVector = new Vector3(inputDirection, verticalVelocity, 0);

        controller.Move(moveVector * Time.deltaTime);
    }
}
