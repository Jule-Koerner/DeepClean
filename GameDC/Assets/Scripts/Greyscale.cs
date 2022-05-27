using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greyscale : MonoBehaviour
{
    [SerializeField] private float radius = 1f;
    private Material RegularMaterial;
    [SerializeField] private Material greyMaterial;
    private bool regular; 

    private Material Grey; 
    // Start is called before the first frame update
    void Start()
    {
        RegularMaterial = this.GetComponent<Renderer>().material;
        regular = true; 
    }

    void makeGrey(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("Hitted" + hitCollider.tag);
            if (hitCollider.tag == "Trash")
            {   
                Color color =  this.GetComponent<Renderer>().material.color;
                float greyValue = (color[0] + color[1] + color[2]) / 3;
                Debug.Log(greyValue);
                greyMaterial.color = new Color(greyValue, greyValue, greyValue);
                this.GetComponent<Renderer>().material=greyMaterial;
                regular = false;
                return;
            }
            
        }

        if (!regular && gameObject.tag != "Grass")
        {
            this.GetComponent<Renderer>().material = RegularMaterial;
            regular = true;
        }

    }

    // private void Update()
    // {
    //     makeGrey(transform.position, radius);
    // }
}