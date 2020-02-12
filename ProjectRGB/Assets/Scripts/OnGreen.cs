using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGreen : MonoBehaviour
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
            if (GetComponent<Renderer>())
                GetComponent<Renderer>().enabled = true;
            
                GetComponent<BoxCollider>().enabled = true;
            if (GetComponent<MeshRenderer>() != null)
                GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            if (GetComponent<Renderer>())
                GetComponent<Renderer>().enabled = false;
            if (GetComponent<BoxCollider>())
                GetComponent<BoxCollider>().enabled = false;
            if (GetComponent<MeshRenderer>() != null)
                GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
