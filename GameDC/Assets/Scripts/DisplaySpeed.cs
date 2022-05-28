using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySpeed : MonoBehaviour
{


    [SerializeField] private List<Image> speeds = new List<Image>();
    [SerializeField] Color[] colors;
    [SerializeField] private int speedRemaining;
    private Color initColor = new Color(255/255,0,0,1);
   // private Color newcolor = new Color(95, 158, 160);

    private int index = 0;

     private void Start()
     {
         for (int i = 0; i < speeds.Count; i++)
         {
             speeds[i].GetComponent<Image>().color = Color.black;
             //speeds[i].color = initColor;
    
         }
        
    }
    // Update is called once per frame
    public void increaseSpeed()
    
    {
       
        if (index != speeds.Count)
        {
            Color col = colors[index];
            col.a = 1;
            //Decrease value of speedRemaining
            //speeds[index].color = Color.white;
            speeds[index].GetComponent<Image>().color = col;
            index++;
            // Color one dot
            
        }
    }

    public void decreaseSpeed()
    {
        if (index >= 0)
        {
            if (index == 0)
            {
                speeds[index].color = Color.black;
            }
            else
            {
                speeds[index - 1].color = Color.black;

            }
            if (index != 0)
            {
                index--;
            }
        }
    }
}
