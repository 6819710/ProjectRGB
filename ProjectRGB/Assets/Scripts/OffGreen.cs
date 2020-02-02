using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffGreen : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerEquip>().equip_green)
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            GetComponent<Renderer>().enabled = true;
            GetComponent<BoxCollider>().enabled = true;
        }
    }
}
