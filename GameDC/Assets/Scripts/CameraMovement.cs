using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] private Transform submarine;

    public Vector3 offset;

    public float smoothSpeed = 0.125f;
    
    
    
    //[SerializeField] private float rotSmoothing;
    


    // Update is called once per frame //Evtl besser mit FixedUpdate
    void LateUpdate()
    {
        if (submarine.gameObject.GetComponent<PlayerMovement>().speed < 0 && offset.z < 0)
        {
            offset.Scale(new Vector3(0, 0, -1));
        }
        else if (submarine.gameObject.GetComponent<PlayerMovement>().speed > 0)
        {
            if (offset.z > 0)
            {
                offset.Scale(new Vector3(0, 0, -1));
            }
        }
        Vector3 camPos = submarine.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, camPos, smoothSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, submarine.rotation, smoothSpeed);
 
        transform.position = smoothedPos;
        transform.LookAt(submarine);


    }
}
