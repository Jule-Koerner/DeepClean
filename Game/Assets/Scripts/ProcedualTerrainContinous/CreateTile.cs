using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreateTile : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Terrain currentTerrain;
    private Terrain lastTerrain;
    private Bounds currentBounds;
    private Terrain a; 
    private TerrainData currentTerrainData; 
    public int depth = 20; //y axis
    public int width = 256;
    public int height = 256;
    private Vector3 startPosition; 

    public float scale = 20f;
    // Update is called once per frame
    void Start()
    {
        currentTerrain = Terrain.CreateTerrainGameObject(new TerrainData()).GetComponent<Terrain>();
        currentTerrainData = currentTerrain.terrainData;
        currentTerrain.terrainData = GenerateTerrain(currentTerrainData);
        Debug.Log("Current terrain data: " + currentTerrainData.bounds);
        startPosition = currentTerrain.transform.position;
        player.transform.position = currentTerrain.terrainData.bounds.center + new Vector3(0, 3, 0); 
        Debug.Log(currentTerrain.terrainData.size);

    }

    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 terrainPosition = currentTerrain.transform.position;
        Debug.Log("Player position: " + playerPosition + " terrain Position: " + terrainPosition);
        if (playerPosition.z >= terrainPosition.z + height / 2)
        {
            Debug.Log("Not in Terrain anymore. Current bounds: " + currentBounds + " | current Position: " + player.transform.position);
            currentTerrainData = new TerrainData();
            
            //set terrain width, height, length
            currentTerrainData.size = new Vector3(width, depth, height);
            lastTerrain = currentTerrain; 
            currentTerrain = Terrain.CreateTerrainGameObject(GenerateTerrain(currentTerrainData)).GetComponent<Terrain>();
            currentTerrain.transform.position = new Vector3(startPosition.x, startPosition.y, player.transform.position.z );
        }
    
        if (lastTerrain != null && lastTerrain.transform.position.z + height / 2 + 10 < playerPosition.z)
        {
            Destroy(lastTerrain.gameObject);
            Debug.Log("Destroyed last game Object");
        }
     
    }
    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width +1 ;
        terrainData.size = new Vector3(width, depth, height);
        
        terrainData.SetHeights(0,0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];    
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }

        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float xCoord = (float) x / width * scale;
        float yCoord = (float) y / height * scale;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
