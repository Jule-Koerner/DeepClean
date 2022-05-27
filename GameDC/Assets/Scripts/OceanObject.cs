using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanObject : MonoBehaviour
{
    [SerializeField] public int instantiateMin = 1;

    [SerializeField] public int instantiateMax = 10;
    public Material regularMaterial; 

    private void Start()
    {
        Destroy(GetComponent<Rigidbody>());
        if (tag != "Flock")
        {
            regularMaterial = GetComponent<Renderer>().material;
        }
    }

}
