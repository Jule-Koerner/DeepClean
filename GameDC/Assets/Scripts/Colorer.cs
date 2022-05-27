using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorer : MonoBehaviour
{

    [SerializeField] private Color[] baseColors;
    // Start is called before the first frame update
    void Start()
    {
        if(transform.childCount > 1)
        {
            foreach(Transform child in transform)
            {
                if (baseColors.Length == 1)
                {
                    child.GetComponent<MeshRenderer>().material.color = baseColors[0]*new Color(Random.Range(0.25f,1f), Random.Range(0.25f,1f), Random.Range(0.25f,1f));
                }
                else
                {
                    child.GetComponent<MeshRenderer>().material.color = baseColors[Random.Range(0,baseColors.Length)]*new Color(Random.Range(0.25f,1f), Random.Range(0.25f,1f), Random.Range(0.25f,1f));
                }
            }
        }
        else
        {
            this.GetComponent<MeshRenderer>().material.color = baseColors[Random.Range(0,baseColors.Length)]*new Color(Random.Range(0.25f,1f), Random.Range(0.25f,1f), Random.Range(0.25f,1f));
        }

        
    }

}
