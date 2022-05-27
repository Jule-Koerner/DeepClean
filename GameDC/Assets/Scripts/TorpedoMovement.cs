using System;
using System.Collections;
using System.Collections.Generic;
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
         // if (Vector3.Distance(submarine.transform.position, transform.position) >distance)
         // {
         //     Destroy(this.gameObject);
         // }
    }

    private void Movement()
    {
        Debug.Log("Hallo ich bin jules torpedo");

        this.transform.position += transform.forward * speed * Time.deltaTime;
        // transform.Translate(transform.forward * speed);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            Destroy(other.gameObject);
        }
    }
}
