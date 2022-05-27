using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject ui;
    [SerializeField] private float speed;
    [SerializeField] private GameObject propeller;
    [SerializeField] private GameObject torpedo;

    private float speedChange = 0.005f;

    private void Start()
    {
        speedChange = speed / 2;

    }

    void Update () { 
        propeller.transform.Rotate(0, speed*2.5f, 0);
        Move();
        Speed();
        Shoot();
    }
    
    void Shoot()
    {
        if (Input.GetKeyDown("space"))
        {
            Instantiate(torpedo, transform.GetChild(0).position, transform.rotation);
        } 
    }
    
    void Speed()
    {
        //Increase Speed
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            speed += speedChange;
            //ui.GetComponent<DisplaySpeed>().increaseSpeed();
            
        }
        
        //Decrease Speed
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            speed -= speedChange;
            //ui.GetComponent<DisplaySpeed>().decreaseSpeed();
        }
        // speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);

    }
    void Move ()
    {
        Vector3 mousePos = Input.mousePosition * 100;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        worldPosition += new Vector3(0, 0, transform.position.z + 30);
        transform.LookAt(worldPosition, Vector3.up);
        var step =  speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, worldPosition, step);
    }
}
