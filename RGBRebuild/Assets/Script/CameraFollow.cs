using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform _target;

    public float _speed = 0.125f;

    public Vector3 _offset;

    private void LateUpdate()
    {
        Vector3 targetPos = _target.position + _offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, targetPos, _speed * Time.deltaTime);
        transform.position = smoothedPos;

        transform.LookAt(_target);
    }
}
