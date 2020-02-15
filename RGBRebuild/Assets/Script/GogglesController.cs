using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GogglesController : MonoBehaviour
{
    // Useable Materials
    [Header("Material Definitions")]
    public Material _whiteGlow;
    public Material _greenGlow;
    public Material _redGlow;
    public Material _blueGlow;

    public Material _whiteHair;
    public Material _greenHair;
    public Material _redHair;
    public Material _blueHair;

    // Refernces
    public SkinnedMeshRenderer bodyRenderer;
    public SkinnedMeshRenderer gogglesRenderer;

    // Flags
    [Header("Flags")]
    public bool _hasGreen;
    public bool _hasRed;
    public bool _hasBlue;
    public bool _isGreen;
    public bool _isRed;
    public bool _isBlue;
    private bool _greenPressed;
    private bool _redPressed;
    private bool _bluePressed;

    // Start is called before the first frame update
    void Start()
    {
        setWhite();
        _greenPressed = false;
        _redPressed = false;
        _bluePressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Green") != 0.0f && _hasGreen && !_isGreen && !_greenPressed)
        {
            _isGreen = true;
            _isRed = false;
            _isBlue = false;
            _greenPressed = true;
            setGreen();
        }
        else if (Input.GetAxis("Green") != 0.0f && _hasGreen && _isGreen && !_greenPressed)
        {
            _isGreen = false;
            _greenPressed = true;
            setWhite();
        }
        else if (Input.GetAxis("Red") != 0.0f && _hasRed && !_isRed && !_redPressed)
        {
            _isGreen = false;
            _isRed = true;
            _isBlue = false;
            _redPressed = true;
            setRed();
        }
        if (Input.GetAxis("Red") != 0.0f && _hasRed && _isRed && !_redPressed)
        {
            _isRed = false;
            _redPressed = true;
            setWhite();
        }
        if (Input.GetAxis("Blue") != 0.0f && _hasBlue && !_isBlue && !_bluePressed)
        {
            _isGreen = false;
            _isRed = false;
            _isBlue = true;
            _bluePressed = true;
            setBlue();
        }
        if (Input.GetAxis("Blue") != 0.0f && _hasBlue && _isBlue && !_bluePressed)
        {
            _isBlue = false;
            _bluePressed = true;
            setWhite();
        }

        if (Input.GetAxis("Green") == 0.0f)
            _greenPressed = false;
        if (Input.GetAxis("Red") == 0.0f)
            _redPressed = false;
        if (Input.GetAxis("Blue") == 0.0f)
            _bluePressed = false;
    }

    void setWhite()
    {
        Material[] bodyMaterials = bodyRenderer.materials;
        bodyMaterials[1] = _whiteGlow;
        bodyMaterials[3] = _whiteHair;
        gogglesRenderer.enabled = false;
        bodyRenderer.materials = bodyMaterials;
    }

    void setGreen()
    {
        Material[] bodyMaterials = bodyRenderer.materials;
        Material[] goggleMaterials = gogglesRenderer.materials;
        bodyMaterials[1] = _greenGlow;
        bodyMaterials[3] = _greenHair;
        goggleMaterials[0] = _greenGlow;
        bodyRenderer.materials = bodyMaterials;
        gogglesRenderer.materials = goggleMaterials;
        gogglesRenderer.enabled = true;
    }

    void setRed()
    {
        Material[] bodyMaterials = bodyRenderer.materials;
        Material[] goggleMaterials = gogglesRenderer.materials;
        bodyMaterials[1] = _redGlow;
        bodyMaterials[3] = _redHair;
        goggleMaterials[0] = _redGlow;
        bodyRenderer.materials = bodyMaterials;
        gogglesRenderer.materials = goggleMaterials;
        gogglesRenderer.enabled = true;
    }

    void setBlue()
    {
        Material[] bodyMaterials = bodyRenderer.materials;
        Material[] goggleMaterials = gogglesRenderer.materials;
        bodyMaterials[1] = _blueGlow;
        bodyMaterials[3] = _blueHair;
        goggleMaterials[0] = _blueGlow;
        bodyRenderer.materials = bodyMaterials;
        gogglesRenderer.materials = goggleMaterials;
        gogglesRenderer.enabled = true;
    }
}
