using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Update is called once per frame
    public Transform player;

    // Update is called once per frame
    void Update () {
        transform.position = player.transform.position + new Vector3(0, 1, -5);
    }
}