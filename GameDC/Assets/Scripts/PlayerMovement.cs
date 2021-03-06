using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject ui;
    [SerializeField] private int numTorpedos = 20;
    private int currentTorpedo = 0;

    [SerializeField] public float speed = 5f;
    private float speedChange = 1f;

    private float maxSpeed = 10f;
    private float minSpeed = 5f;

    private float rotationSpeed = 5;


    private float rot = 0f;
    private float rotY = 0f;
    private Rigidbody rb;

    //[SerializeField] private Transform startRay;
    [SerializeField] private GameObject torpedo;


    public float mouseAxisX;
    public float mouseAxisY;
    [SerializeField] private GameObject propeller;
    private Quaternion propellerRotation;


    void Start()
    {
        propellerRotation = propeller.transform.rotation;
        rb = GetComponent<Rigidbody>();
        //speedChange = speed;

        ui.GetComponent<DisplaySpeed>().torpedoCount.text = "" + numTorpedos;
    }

    // Update is called once per frame
    void Update()
    {
        // propeller.transform.rotation = Quaternion.Euler(propellerRotation.x + speed, propellerRotation.y, propellerRotation.z);
        // propellerRotation = propeller.transform.rotation;
        propeller.transform.Rotate(0, speed, 0);
        Speed();
        Movement();
        Shoot();
    }

    private void FixedUpdate()
    {
        //Speed();
        //rb.MovePosition(transform.position + transform.TransformDirection(transform.forward) * Time.deltaTime * speed);
        transform.position += transform.forward * speed * Time.deltaTime;
    }


    void Speed()
    {
        //Increase Speed
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            speed += 1;

            ui.GetComponent<DisplaySpeed>().increaseSpeed();
            Debug.Log("END UIIIII");

        }

        //Decrease Speed
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            speed -= speedChange;
            ui.GetComponent<DisplaySpeed>().decreaseSpeed();
        }
        speed = Mathf.Clamp(speed, 5, maxSpeed);

    }

    void Movement()
    {
        //Movement
        //transform.Translate(transform.forward * 0.0005f);
        //rb.AddRelativeForce(-transform.forward * 0.05f);
        // Move position ist bisschen wie motor, moved genau da hin wo ich will (full direct contro, not affected by physics itself but works for affecting others)
        //rb.MovePosition(transform.position + transform.TransformDirection(transform.forward) * speed * Time.deltaTime);
       // transform.Translate(transform.forward * speed * Time.deltaTime);
        //Rotation
         mouseAxisX = Input.GetAxis("Mouse X");
         rot += mouseAxisX * rotationSpeed/2;
         //rot = Mathf.Clamp(rot, -100f, 100f);
         //transform.eulerAngles = new (90f, rot, 0f);
         //rb.rotation = Quaternion.Euler(90f, rot, 0f);

         //Rotation
         mouseAxisY = Input.GetAxis("Mouse Y");
         rotY += mouseAxisY * rotationSpeed/2;

        // Debug.Log("angle" + rotY);
         //rot = Mathf.Clamp(rot, -100f, 100f);
         //transform.eulerAngles = new (90f, rot, 0f);

         rot = Mathf.Clamp(rot, -90, 90);
         //rotY = Mathf.Clamp(rot, -45, 90);

         rb.MoveRotation(Quaternion.Euler(-rotY, rot, 0f));
         // if ((transform.forward.z < 0 && speed > 0) || transform.forward.z > 0 && speed < 0)
         // {
         //     speed *= -1;
         //     // transform.forward = new Vector3(transform.forward.x * -1, transform.forward.y * -1, transform.forward.z);
         //     // rb.rotation = Quaternion.Euler(rotY, rot,  0f);
         //
         // }
         // // Debug.Log(transform.forward +" speed: "+ speed);

         // Debug.Log(rb.rotation);

         // //Upwards movement
         // if(Input.GetKey(KeyCode.UpArrow))
         // {
         //     transform.Translate(Vector3.up * 0.005f, Space.World);
         // }
         //
         // //Downwards movement
         // if(Input.GetKey(KeyCode.DownArrow))
         // {
         //     transform.Translate(Vector3.down * 0.005f, Space.World);
         // }



    }
    void Shoot()
    {
        if (Input.GetKeyDown("space"))
        {
            if (numTorpedos != 0)
            {
                Instantiate(torpedo, transform.GetChild(0).position, transform.rotation);
                numTorpedos--;
                ui.GetComponent<DisplaySpeed>().TorpedoCount(numTorpedos);
            }
        }
        else
        {
            //Final Scene
        }
    }
}
