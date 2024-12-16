using System.Collections.Generic;
using UnityEngine;

public class TilePlacer : MonoBehaviour
{
    [Header("Tile Prefabs")]
    public GameObject endTile;
    public List<GameObject> middleTiles;

    [Header("Tile Placement Settings")]
    public int middleTileAmount = 5;
    public float defaultTileLength = 100f;

    private Vector3 currentPosition;
    private float lastLength; // Half-length not full

    void Awake()
    {
        PlaceTiles();
    }

    void PlaceTiles()
    {
        if (endTile == null)
        {
            Debug.LogError("EndTile must be assigned!");
            return;
        }

        if (middleTiles.Count == 0 && middleTileAmount > 0)
        {
            Debug.LogError("No middle tiles assigned!");
        }

        currentPosition = transform.position;
        lastLength = GetTileLength(gameObject) / 2f;

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
        Instantiate(tilePrefab, currentPosition, Quaternion.identity, transform.parent);
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
        Debug.LogWarning($"No Renderer found on {tilePrefab.name}!");
        return defaultTileLength;
    }
}