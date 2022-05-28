using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySpeed : MonoBehaviour
{


    [SerializeField] private List<Image> speeds = new List<Image>();
    [SerializeField] Color[] colors;
    private int speedRemaining;

    public TextMeshProUGUI torpedoCount;
    private int index = 0;



     private void Start()
     {
        // torpedoCount.text = "";

         for (int i = 0; i < speeds.Count; i++)
         {
             speeds[i].GetComponent<Image>().color = Color.black;

         }


     }
    // Update is called once per frame
    public void increaseSpeed()
    {
        if (index != speeds.Count)
        {
            Color col = colors[index];
            col.a = 1;
            speeds[index].GetComponent<Image>().color = col;
            index++;
        }
    }

    public void decreaseSpeed()
    {

        if (index != 0)
        {
            speeds[index - 1].color = Color.black;
            index--;

        }
        else
        {
            speeds[index].color = Color.black;
        }
    }


    public void TorpedoCount(int numTor)
    {
        torpedoCount.text = "" + numTor;

    }
}
