﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerEquip : MonoBehaviour
{
    public bool has_red;
    public bool has_green;
    public bool has_blue;

    public bool equip_red;
    public bool equip_green;
    public bool equip_blue;

    public bool red_pressed;
    public bool green_pressed;
    public bool blue_pressed;

    public AudioMixerSnapshot snapshot_1;
    public AudioMixerSnapshot snapshot_2;
    public AudioMixerSnapshot snapshot_3;
    public AudioMixerSnapshot snapshot_4;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("SetRed") != 0.0f && has_red && !red_pressed)
        {
            if(equip_red)
            {
                equip_red = false;
            }
            else
            {
                equip_red = true;
                equip_green = false;
                equip_blue = false;
            }

            red_pressed = true;
        }

        if (Input.GetAxis("SetGreen") != 0.0f && has_green && !green_pressed)
        {
            if (equip_green)
            {
                equip_green = false;
            }
            else
            {
                equip_red = false;
                equip_green = true;
                equip_blue = false;
            }
            green_pressed = true;
        }

        if (Input.GetAxis("SetBlue") != 0.0f && has_blue && !blue_pressed)
        {
            if (equip_blue)
            {
                equip_blue = false;
            }
            else
            {
                equip_red = false;
                equip_green = false;
                equip_blue = true;
            }
            blue_pressed = true;
        }

        if (Input.GetAxis("SetRed") == 0.0f)
            red_pressed = false;
        if (Input.GetAxis("SetGreen") == 0.0f)
            green_pressed = false;
        if (Input.GetAxis("SetBlue") == 0.0f)
            blue_pressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "RedGoggles":
                has_red = true;
                other.gameObject.GetComponent<Renderer>().enabled = false;
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                snapshot_3.TransitionTo(0.0f);
                break;
            case "GreenGoggles":
                has_green = true;
                other.gameObject.GetComponent<Renderer>().enabled = false;
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                snapshot_2.TransitionTo(0.0f);
                break;
            case "BlueGoggles":
                has_blue = true;
                other.gameObject.GetComponent<Renderer>().enabled = false;
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                snapshot_4.TransitionTo(0.0f);
                break;
        }
    }
}
