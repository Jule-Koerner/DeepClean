using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LevelMaker : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;
    [SerializeField] private Terrain terrain; 
    [SerializeField] private LayerMask terrainLayerMask; 
    private float terrainWidth;
    private float terrainLength;

    private float xTerrainPos;
    private float zTerrainPos;
    

    // Start is called before the first frame update

    void Awake()
    {
        //Get terrain size
        terrainWidth = terrain.terrainData.size.x;
        terrainLength = terrain.terrainData.size.z;

        //Get terrain position
        xTerrainPos = terrain.transform.position.x;
        zTerrainPos = terrain.transform.position.z;

        RaycastHit hit;

        foreach (var prefab in prefabs)
        {
            for (int i = 0; i < Random.Range(prefab.GetComponent<OceanObject>().instantiateMin, prefab.GetComponent<OceanObject>().instantiateMax); i++)
            {
                //Generate random x,z,y position on the terrain
                float randX = UnityEngine.Random.Range(xTerrainPos, xTerrainPos + terrainWidth);
                float randZ = UnityEngine.Random.Range(zTerrainPos, zTerrainPos + terrainLength);
                Vector3 position = new Vector3(randX, 0, randZ);
                //Do a raycast along Vector3.down -> if you hit something the result will be given to you in the "hit" variable
                //This raycast will only find results between +-100 units of your original"position" (ofc you can adjust the numbers as you like)
                if (Physics.Raycast (position + new Vector3(0, 100.0f, 0), Vector3.down, out hit, 400.0f, terrainLayerMask)) {
                    GameObject instantiatedObject = Instantiate (prefab, hit.point, prefab.transform.rotation);
                    instantiatedObject.transform.localScale.Scale(new Vector3(Random.Range(0.5f, 2), Random.Range(0.5f, 2), Random.Range(0.5f, 2)));

                    // if(instantiatedObject.GetComponent<BoxCollider>().)
                } else {
                    Debug.Log ("there seems to be no ground at this position");
                }
                
            }
        }
        
    }

    
}
