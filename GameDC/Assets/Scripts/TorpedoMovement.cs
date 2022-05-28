using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class TorpedoMovement : MonoBehaviour
{
    [SerializeField] public float speed = 0.5f;

    public GameObject submarine;
    [SerializeField] private int  distance = 4000;



     private void Update()
     {

         Movement();
         if (Vector3.Distance(submarine.transform.position, transform.position) >distance)
         {
             Destroy(this.gameObject);
         }
    }

    private void Movement()
    {
        this.transform.position += transform.forward * speed * Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            other.gameObject.GetComponent<Trash>().Recolor();
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
