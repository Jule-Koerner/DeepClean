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
    
    [SerializeField] GameObject[] prefabs;
    [SerializeField] private LayerMask terrainLayerMask; 
    [SerializeField] private Material terrainMaterial; 
    private float xTerrainPos;
    private float zTerrainPos;

    private Terrain currentTerrain;
    private Terrain lastTerrain;
    private Bounds currentBounds;
    private TerrainData currentTerrainData; 
    
    public int depth = 20; //y axis
    public int width = 256;
    public int height = 256;
    private Vector3 startPosition;
    private List<GameObject> currentTerrainObjects;
    private int lastNInstantiatedObjects = 0; 
    
    private float[,] currentHeights;

    public float scale = 20f;
    // Update is called once per frame
    void Start()
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
        if (playerPosition.z >= terrainPosition.z + height / 2)
        {
            currentTerrainData = new TerrainData();
            
            //set terrain width, height, length
            CreateTileInstance();
        }
        
        if (lastTerrain != null && lastTerrain.transform.position.z + height / 2 + 10 < playerPosition.z)
        {
            Destroy(lastTerrain.gameObject);

            
        }
     
    }

    void CreateTileInstance()
    {
        for (int i = 0; i < lastNInstantiatedObjects; i++)
        {
            Destroy(currentTerrainObjects[i]);
        }
        currentTerrainData.size = new Vector3(width, depth, height);
        lastTerrain = currentTerrain;
        currentTerrain = Terrain.CreateTerrainGameObject(GenerateTerrain(currentTerrainData)).GetComponent<Terrain>();
        currentTerrain.materialTemplate = terrainMaterial;
        currentTerrain.transform.position = new Vector3(startPosition.x, startPosition.y, player.transform.position.z );
        PopulateTerrain();
    }

    void PopulateTerrain()
    {
        // RaycastHit hit;
        foreach (var prefab in prefabs)
        {
            for (int i = 0; i < Random.Range(prefab.GetComponent<OceanObject>().instantiateMin, prefab.GetComponent<OceanObject>().instantiateMax); i++)
            {
                //Generate random x,z,y position on the terrain
                float randX = UnityEngine.Random.Range(currentTerrain.transform.position.x, currentTerrain.transform.position.x + width);
                float randZ = UnityEngine.Random.Range(currentTerrain.transform.position.z, currentTerrain.transform.position.z + height);
                Vector3 position = new Vector3(randX, Random.Range(0,5), randZ);

                GameObject instantiatedObject = Instantiate (prefab, position, prefab.transform.rotation);
                instantiatedObject.transform.localScale.Scale(new Vector3(Random.Range(0.5f, 2), Random.Range(0.5f, 2), Random.Range(0.5f, 2)));
                currentTerrainObjects.Add(instantiatedObject);
                lastNInstantiatedObjects++;
                Debug.Log("Prefab was added to list. current n: " + lastNInstantiatedObjects);  

            }
        }
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
        float start = 2f; 
        float[,] heights = new float[width, height];    
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float scaler = scalingFactor(height, y, start);
                heights[x, y] = CalculateHeight(x, y) * scaler;
            }
        }

        currentHeights = heights;
        return heights;
    }

    float scalingFactor(int maxY, float value, float scaleMax)
    {
        return Mathf.Pow(value, 2) / Mathf.Pow(maxY / 2, 2) * scaleMax - maxY * value / Mathf.Pow(maxY / 2, 2) *
            scaleMax + scaleMax + 0.3f;
    }

    float CalculateHeight(int x, int y)
    {
        float xCoord = (float) x / width * Random.Range(scale - scale * 0.1f, scale);
        float yCoord = (float) y / height * Random.Range(scale - scale * 0.1f, scale);;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
