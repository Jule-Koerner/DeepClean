using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySpeed : MonoBehaviour
{


    [SerializeField] private List<Image> speeds = new List<Image>();
    
    [SerializeField] private int speedRemaining;
    [SerializeField] private Color initColor = Color.gray;
    [SerializeField] private Color color = Color.green;

    private int index = 0;

    private void Start()
    {
        for (int i = 0; i < speeds.Count; i++)
        {
            speeds[i].color = initColor;
        }
        
    }
    // Update is called once per frame
    public void increaseSpeed()
    {
        if (index != speeds.Count)
        {
            //Decrease value of speedRemaining
            speeds[index].color = color;
            index++;
            // Color one dot
            
        }
    }

    public void decreaseSpeed()
    {
        if (index >= 0)
        {
            speeds[index - 1].color = initColor;
            index--;
        }
    }

    //Only for Testing!

}
