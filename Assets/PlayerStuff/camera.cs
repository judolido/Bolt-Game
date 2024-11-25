using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class camera : MonoBehaviour
{

    public Transform player;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position;


    }

    // Update is called once per frame
    void Update()
    {
        transform.position = offset + player.position;
    }
}
