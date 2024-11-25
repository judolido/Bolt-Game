using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetSpawner : MonoBehaviour
{
    public GameObject player; // Reference to the player object
    public float spawnDistance = 50f; // Distance ahead of the player to spawn streets
    public int initialSpawnCount = 5; // Number of streets to spawn initially
    public float streetLength = 10f; // Length of a single street prefab
    public string streetFolder = "StreetPrefabs"; // Folder name inside Resources

    private Queue<GameObject> activeStreets = new Queue<GameObject>();
    private List<GameObject> streetPrefabs;
    private Vector3 nextSpawnPosition;

    void Start()
    {
        // Load all street prefabs from the Resources folder
        streetPrefabs = new List<GameObject>(Resources.LoadAll<GameObject>("WorldLogic"));

        if (streetPrefabs.Count == 0)
        {
            Debug.LogError("No street prefabs found in the specified folder!");
            return;
        }

        // Set initial spawn position
        nextSpawnPosition = transform.position;

        // Spawn initial streets
        for (int i = 0; i < initialSpawnCount; i++)
        {
            SpawnStreet();
        }
    }

    void Update()
    {
        // Check if we need to spawn a new street
        if (Vector3.Distance(player.transform.position, nextSpawnPosition) < spawnDistance)
        {
            SpawnStreet();
            RemoveOldStreet();
        }
    }

    void SpawnStreet()
    {
        // Choose a random street prefab
        GameObject randomStreet = streetPrefabs[Random.Range(0, streetPrefabs.Count)];
        // Spawn the street at the next position
        GameObject newStreet = Instantiate(randomStreet, nextSpawnPosition, Quaternion.identity);
        // Add the new street to the queue
        activeStreets.Enqueue(newStreet);
        // Update the next spawn position
        nextSpawnPosition += Vector3.forward * streetLength;
    }

    void RemoveOldStreet()
    {
        // If we have more streets than needed, remove the oldest one
        if (activeStreets.Count > initialSpawnCount)
        {
            GameObject oldStreet = activeStreets.Dequeue();
            Destroy(oldStreet);
        }
    }
}
