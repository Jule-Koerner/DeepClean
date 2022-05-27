using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Material greyMaterial;
    [SerializeField] private List<Collider> changedObjects; 
    void Start()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 100);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("Hitted" + hitCollider.tag);
            if (hitCollider.tag == "Fish" || hitCollider.tag == "Plant")
            {   
                Color color =  hitCollider.GetComponent<Renderer>().material.color;
                float greyValue = (color[0] + color[1] + color[2]) / 3;
                Debug.Log(greyValue);
                greyMaterial.color = new Color(greyValue, greyValue, greyValue);
                hitCollider.GetComponent<Renderer>().material=greyMaterial;
                changedObjects.Add(hitCollider);
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var changedObject in changedObjects)
        {
            changedObject.GetComponent<Renderer>().material=changedObject.GetComponent<OceanObject>().regularMaterial;
        }
        Destroy(this.gameObject);
    }
}
