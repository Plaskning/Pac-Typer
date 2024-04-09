using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    //Camera _camera = Camera.main;
    void Update()
    {
        transform.forward = Camera.main.transform.forward;
        //transform.LookAt(transform.position + _camera.transform.position * Vector3.forward, _camera.transform.rotation * Vector3.up);
    }
}
