using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanObject : MonoBehaviour
{
    [SerializeField] public int instantiateMin = 1;

    [SerializeField] public int instantiateMax = 10;

    private void Start()
    {
        Destroy(GetComponent<Rigidbody>());
    }

    void OnCollisionEnter(Collision collision)
    {
        //Output the Collider's GameObject's name
        if (collision.gameObject.tag == "Terrain")
        {
            Destroy(GetComponent<Rigidbody>());
            // GetComponent<Rigidbody>().enable = false; 
        }
        else if (collision.gameObject.tag == "Player")
        {
            gameObject.AddComponent<Rigidbody>();
        }
    }

    private void Update()
    {
        if (transform.position.y <= -5)
        {
            Destroy(gameObject);
        }
    }
}
