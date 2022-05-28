using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Trash : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Material greyMaterial;
    [SerializeField] private Material coloredMaterial;
    private List<Collider> changedObjects = new List<Collider>();

    // private void Start()
    // {
    // }

    public void Start()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 50);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Plant")
            {
                MakeGrey(hitCollider);
            }

            if (hitCollider.tag == "Fish")
            {
                MakeGrey(hitCollider);
            }
        }
    }

    void MakeGrey(Collider hitCollider)
    {
        changedObjects.Add(hitCollider);
        Color color =  hitCollider.GetComponent<Renderer>().material.color;
        float greyValue = (color[0] + color[1] + color[2]) / 3;
        greyMaterial.color = new Color(greyValue, greyValue, greyValue);
        hitCollider.GetComponent<MeshRenderer>().material=greyMaterial;
        Debug.Log("Count after greying out: " + changedObjects.Count);
    }

    public void Recolor()
    {
        Debug.Log("Count when recolering greying out: " + changedObjects.Count);
        foreach (var changedObject in changedObjects)
        {
            Debug.Log("Decolored: " + changedObject);
            changedObject.GetComponent<MeshRenderer>().material=changedObject.GetComponent<OceanObject>().regularMaterial;
            // changedObject.GetComponent<Renderer>().material=coloredMaterial;
        }
    }
}
