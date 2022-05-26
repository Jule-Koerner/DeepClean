using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TorpedoMovement : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;

    [SerializeField] private GameObject submarine;
    [SerializeField] private int  distance = 40;
    
    

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
        //Debug.Log("Hallo ich bin jules torpedo");

        this.transform.position += transform.forward * speed;
        // transform.Translate(transform.forward * speed);
    }

    
    



 
}
