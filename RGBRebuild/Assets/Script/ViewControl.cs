using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewControl : MonoBehaviour
{
    private GameObject _player;

    public bool _onWhite;
    public bool _onGreen;
    public bool _onRed;
    public bool _onBlue;

    public bool _offWhite;
    public bool _offGreen;
    public bool _offRed;
    public bool _offBlue;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.GetComponent<GogglesController>()._isGreen)
        {
            if (_onGreen)
            {
                setOn();
            }
            else if (_offGreen)
            {
                setOff();
            }
        }
        else if(_player.GetComponent<GogglesController>()._isRed)
        {
            if (_onRed)
            {
                setOn();
            }
            else if (_offRed)
            {
                setOff();
            }
        }
        else if(_player.GetComponent<GogglesController>()._isBlue)
        {
            if (_onBlue)
            {
                setOn();
            }
            else if (_offBlue)
            {
                setOff();
            }
        }
        else
        {
            if (_onWhite)
            {
                setOn();
            }
            else if (_offWhite)
            {
                setOff();
            }
        }

    }

    void setOn()
    {
        if (GetComponent<Renderer>() != null)
            GetComponent<Renderer>().enabled = true;
        if (GetComponent<MeshRenderer>() != null)
            GetComponent<MeshRenderer>().enabled = true;
        if (GetComponent<SkinnedMeshRenderer>() != null)
            GetComponent<SkinnedMeshRenderer>().enabled = true;
        if (GetComponent<BoxCollider>() != null)
            GetComponent<BoxCollider>().enabled = true;
    }

    void setOff()
    {
        if (GetComponent<Renderer>() != null)
            GetComponent<Renderer>().enabled = false;
        if (GetComponent<MeshRenderer>() != null)
            GetComponent<MeshRenderer>().enabled = false;
        if (GetComponent<SkinnedMeshRenderer>() != null)
            GetComponent<SkinnedMeshRenderer>().enabled = false;
        if (GetComponent<BoxCollider>() != null)
            GetComponent<BoxCollider>().enabled = false;
    }
}
