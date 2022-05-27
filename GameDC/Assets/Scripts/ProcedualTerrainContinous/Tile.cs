using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject trashPrefab;
    [SerializeField] private int maxTrash; 
    [SerializeField] private int maxObjectsPerTile = 15; 
    private int objectsPertileCounter = 0;
    
    [SerializeField] GameObject[] prefabs;
    [SerializeField] private LayerMask terrainLayerMask; 
    [SerializeField] private Material terrainMaterial; 
    private float xTerrainPos;
    private float zTerrainPos;

    private Terrain currentTerrain;
    private Terrain lastTerrain;
    private Bounds currentBounds;
    private TerrainData currentTerrainData;
    private List<GameObject> allTiles;

    public int depth = 20; //y axis
    public int width = 256;
    public int height = 256;
    private Vector3 startPosition;
    private List<GameObject> currentTerrainObjects;
    private int lastNInstantiatedObjects = 0; 
    
    private float[,] currentHeights;

    public float scale = 20f;
    void CreateTileInstance()
    {
        
        currentTerrainData.size = new Vector3(width, depth, height);
        lastTerrain = currentTerrain;
        currentTerrain = Terrain.CreateTerrainGameObject(GenerateTerrain(currentTerrainData)).GetComponent<Terrain>();
        currentTerrain.materialTemplate = terrainMaterial;
        currentTerrain.transform.position = new Vector3(startPosition.x, startPosition.y, player.transform.position.z );
        PopulateTerrain();
    }

    void PopulateTerrain()
    {
        int counter = 0; 
        for (int j = 0; j < maxObjectsPerTile; j++)
        {
            GameObject prefab = prefabs[UnityEngine.Random.Range(0, prefabs.Length)];
            for (int i = 0; i < Random.Range(prefab.GetComponent<OceanObject>().instantiateMin, prefab.GetComponent<OceanObject>().instantiateMax); i++)
            {
                //Generate random x,z,y position on the terrain
                SetPrefab(prefab);
            }
        }
  
        for (int i = 0; i < maxTrash; i++)
        {
            SetPrefab(trashPrefab);   
        }
    }

    void SetPrefab(GameObject prefab)
    {
        float randX = UnityEngine.Random.Range(currentTerrain.transform.position.x, currentTerrain.transform.position.x + width);
        float randZ = UnityEngine.Random.Range(currentTerrain.transform.position.z, currentTerrain.transform.position.z + height);
        Vector3 position = new Vector3(randX, 0, randZ);
        position.y = currentTerrain.SampleHeight(position);
                
        if (prefab.tag == "Flock")
        {
            // position.x = UnityEngine.Random.Range(currentTerrain.terrainData.bounds.center.x-10, currentTerrain.terrainData.bounds.center.x+10 ); 
            position.y += 5;
        }
                
        GameObject instantiatedObject = Instantiate (prefab, position, prefab.transform.rotation);
        instantiatedObject.transform.localScale.Scale(new Vector3(Random.Range(0.5f, 2), Random.Range(0.5f, 2), Random.Range(0.5f, 2)));
        currentTerrainObjects.Add(instantiatedObject);
        lastNInstantiatedObjects++;
        Debug.Log("Prefab was added to list. current n: " + lastNInstantiatedObjects);  
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

        currentHeights = heights;
        return heights;
    }

    float scalingFactor(int maxY, float value, float scaleMax)
    {
        return Mathf.Pow(value, 2) / Mathf.Pow(maxY / 2, 2) * scaleMax - maxY * value / Mathf.Pow(maxY / 2, 2) *
            scaleMax + scaleMax + 0.1f;
    }

    float CalculateHeight(int x, int y)
    {
        float xCoord = (float) x / width * Random.Range(scale - scale * 0.1f, scale);
        float yCoord = (float) y / height * Random.Range(scale - scale * 0.1f, scale);;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
