using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float inputDirection;
    private CharacterController controller;
    private Vector3 moveVector;

    public bool debug;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        inputDirection = Input.GetAxis("Horizontal");
        if(debug)
               Debug.Log(inputDirection);

        moveVector = new Vector3(inputDirection / 10, 0, 0);

        controller.Move(moveVector);
    }
}
