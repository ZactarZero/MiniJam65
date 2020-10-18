using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;
    public Transform target;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
