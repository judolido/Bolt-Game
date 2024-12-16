using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;


    }

    // Update is called once per frame
    void Update()
    {
        transform.position = offset + target.position;
    }
}