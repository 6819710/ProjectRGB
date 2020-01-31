using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 offset;

    private Vector3 camera_current;
    private Vector3 camera_movement;

    private Vector3 player_current;

    private float x;
    private float y;

    public GameObject player;

    public float x_offset;
    public float y_offset_up;
    public float y_offset_down;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;    
    }

    // Late Update is called after each frame
    void LateUpdate()
    {
        camera_current = transform.position - offset;
        player_current = player.transform.position;
        if (camera_current.x - player_current.x < -x_offset)
            x = player_current.x - x_offset;
        else if (camera_current.x - player_current.x > x_offset)
            x = player_current.x + x_offset;

        if (camera_current.y - player_current.y < -y_offset_up)
            y = player_current.y - y_offset_up;
        else if (camera_current.y - player_current.y > y_offset_down)
            y = player_current.y + y_offset_down;

        camera_movement = new Vector3(x, y) + offset;

        transform.position = camera_movement;
    }
}
