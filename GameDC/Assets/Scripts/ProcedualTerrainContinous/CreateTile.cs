using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32.SafeHandles;
using UnityEngine;
using Random = UnityEngine.Random;


public class CreateTile : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject trashPrefab;
    [SerializeField] private int maxTrash; 
    [SerializeField] private int maxObjectsPerTile = 15; 
    private int objectsPertileCounter = 0;
    
    [SerializeField] GameObject[] prefabs;
    [SerializeField] private Material terrainMaterial; 
    private float xTerrainPos;
    private float zTerrainPos;

    private Terrain currentTerrain;
    private List<Terrain> lastTerrains;
    private Bounds currentBounds;
    private TerrainData currentTerrainData; 
    
    public int depth = 20; //y axis
    public int width = 256;
    public int height = 256;
    private Vector3 startPosition;
    private List<GameObject> currentTerrainObjects;
    
    public float scale = 20f;
    // Update is called once per frame
    void Awake()
    {
        currentTerrainObjects = new List<GameObject>(); 
        currentTerrain = Terrain.CreateTerrainGameObject(new TerrainData()).GetComponent<Terrain>();
        currentTerrainData = currentTerrain.terrainData;
        currentTerrain.terrainData = GenerateTerrain(currentTerrainData);
        startPosition = currentTerrain.transform.position;
        player.transform.position = currentTerrain.terrainData.bounds.center + new Vector3(0, 3, 0); 
        currentTerrain.materialTemplate = terrainMaterial;

        Debug.Log(currentTerrain.terrainData.size);

    }

    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 terrainPosition = currentTerrain.transform.position;
        // Debug.Log("Player position: " + playerPosition + " terrain Position: " + terrainPosition);
        if (playerPosition.z >= terrainPosition.z - width)
        {
            currentTerrainData = new TerrainData();
            
            //set terrain width, height, length
            CreateTileInstance();
        }
        
        DeleteInstance();
     
    }

    void DeleteInstance()
    {
        if (lastTerrains.Count > 0 && lastTerrains[0] != null && lastTerrains[0].transform.position.z + height + 15 < player.transform.position.z)
        {
            Destroy(lastTerrains[0].gameObject);
            lastTerrains.RemoveAt(0);
            // for (int i = 0; i < lastNInstantiatedObjects; i++)
            // {
            //     Destroy(currentTerrainObjects[i]);
            // }
            // lastNInstantiatedObjects = 0; 
            foreach (var terrainObject in currentTerrainObjects)
            {
                if (terrainObject && terrainObject.transform.position.z + 30 < player.transform.position.z)
                {
                    if (terrainObject.tag == "Flock")
                    {
                        foreach (var fish in terrainObject.GetComponent<Flock>().allUnits)
                        {
                            Destroy(fish.gameObject);
                        }
                        Destroy(terrainObject);
                    }

                    Destroy(terrainObject);
                }
            }
        }
    }

    void CreateTileInstance()
    {
        lastTerrains.Add(currentTerrain);
        currentTerrainData.size = new Vector3(width, depth, height);
        currentTerrain = Terrain.CreateTerrainGameObject(GenerateTerrain(currentTerrainData)).GetComponent<Terrain>();
        currentTerrain.materialTemplate = terrainMaterial;
        currentTerrain.transform.position = new Vector3(startPosition.x, startPosition.y, lastTerrains.Last().transform.position.z + height -10);
        PopulateTerrain();
    }

    void PopulateTerrain()
    {
        for (int j = 0; j < maxObjectsPerTile; j++)
        {
            GameObject prefab = prefabs[UnityEngine.Random.Range(0, prefabs.Length)];
            for (int i = 0; i < Random.Range(prefab.GetComponent<OceanObject>().instantiateMin, prefab.GetComponent<OceanObject>().instantiateMax); i++)
            {
                //Generate random x,z,y position on the terrain
                SetPrefab(prefab);
            }
        }
  
        // for (int i = 0; i < maxTrash; i++)
        // {
        //     SetPrefab(trashPrefab);   
        // }
        SetPrefab(trashPrefab);   

    }

    void SetPrefab(GameObject prefab)
    {
        float randX = UnityEngine.Random.Range(currentTerrain.transform.position.x + width / 2 - 18, currentTerrain.transform.position.x + width / 2 + 18);;
        float randZ = UnityEngine.Random.Range(currentTerrain.transform.position.z, currentTerrain.transform.position.z + height);
        Vector3 position = new Vector3(randX, 0, randZ);
        if (prefab.tag == "Trash")
        {
            position.x = UnityEngine.Random.Range(currentTerrain.transform.position.x + width / 2 - 10, currentTerrain.transform.position.x + width / 2 + 10);
        }

        position.y = currentTerrain.SampleHeight(position);
                
        if (prefab.tag == "Flock")
        {
            // position.x = UnityEngine.Random.Range(currentTerrain.terrainData.bounds.center.x-10, currentTerrain.terrainData.bounds.center.x+10 ); 
            position.y += 5;
        }

      
        GameObject instantiatedObject = Instantiate (prefab, position, prefab.transform.rotation);
        // instantiatedObject.transform.localScale.Scale(new Vector3(Random.Range(0.5f, 1), Random.Range(0.5f, 1), Random.Range(0.5f, 1)));
        instantiatedObject.transform.lossyScale.Scale(new Vector3(Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f))); 
        currentTerrainObjects.Add(instantiatedObject);
    }
    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width +1 ;
        // terrainData.size = new Vector3(width, Random.Range(depth - depth * 0.5f, depth), height);
        terrainData.size = new Vector3(width, depth, height);
        
        terrainData.SetHeights(0,0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float start = Random.Range(0.7f,2); 
        float[,] heights = new float[width, height];    
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float scaler = scalingFactor(height, y, start);
                heights[x, y] = CalculateHeight(x, y) * scaler;
            }
        }

        return heights;
    }

    float scalingFactor(int maxY, float value, float scaleMax)
    {
        return Mathf.Pow(value, 2) / Mathf.Pow(maxY / 2, 2) * scaleMax - maxY * value / Mathf.Pow(maxY / 2, 2) *
            scaleMax + scaleMax;
    }

    float CalculateHeight(int x, int y)
    {
        float xCoord = (float) x / width * Random.Range(scale - scale * 0.1f, scale);
        float yCoord = (float) y / height * Random.Range(scale - scale * 0.1f, scale);;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
