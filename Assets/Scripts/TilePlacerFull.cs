using System.Collections.Generic;
using UnityEngine;

public class TilePlacerFull : MonoBehaviour // Places StartTile as well
{
    [Header("Tile Prefabs")]
    public GameObject startTile;
    public GameObject endTile;
    public List<GameObject> middleTiles;

    [Header("Tile Placement Settings")]
    public int middleTileAmount = 5;
    public float defaultTileLength = 100f;

    private Vector3 currentPosition;
    private float lastLength;

    void Awake()
    {
        PlaceTiles();
    }

    void PlaceTiles()
    {
        if (startTile == null || endTile == null)
        {
            Debug.LogError("StartTile and EndTile must be assigned!");
            return;
        }

        currentPosition = transform.position;
        lastLength = GetTileLength(startTile) / 2f;
        
        PlaceTile(startTile);

        for (int i = 0; i < middleTileAmount; i++)
        {
            GameObject randomMiddleTile = middleTiles[Random.Range(0, middleTiles.Count)];
            PlaceTile(randomMiddleTile);
        }
        
        PlaceTile(endTile);
    }

    private void PlaceTile(GameObject tilePrefab)
    {
        float tileLength = GetTileLength(tilePrefab);
        currentPosition += new Vector3(0, 0, (lastLength + tileLength / 2));
        GameObject tileInstance = Instantiate(tilePrefab, currentPosition, Quaternion.identity, transform);
        lastLength = tileLength / 2f;
    }

    private float GetTileLength(GameObject tilePrefab)
    {
        // Check if the prefab has a Renderer to calculate the actual bounds
        Renderer tileRenderer = tilePrefab.GetComponentInChildren<Renderer>();
        if (tileRenderer != null)
        {
            return tileRenderer.bounds.size.z;
        }
        return defaultTileLength;
    }
}