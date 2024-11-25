using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject[] roadPrefabs;  // Array of road section prefabs (assign in the Inspector)
    public Transform player;         // Reference to the player (assign in the Inspector)
    public float spawnTriggerDistance = 30f; // Distance the player must travel to spawn the next prefab
    private float nextSpawnZ = 0f;           // Tracks the next Z position to spawn the prefab
    private float lastPlayerZ;              // Tracks the player's Z position

    void Start()
    {
        // Initialize the last player Z position
        lastPlayerZ = player.position.z;

        // Spawn the first road section
        SpawnRoad();
    }

    void Update()
    {
        // Check if the player has traveled enough distance to trigger the next spawn
        if (player.position.z >= lastPlayerZ + spawnTriggerDistance)
        {
            SpawnRoad();
            lastPlayerZ += spawnTriggerDistance; // Update the last player Z position
        }
    }

    void SpawnRoad()
    {
        // Pick a random prefab
        GameObject roadToSpawn = roadPrefabs[Random.Range(0, roadPrefabs.Length)];

        // Spawn the prefab at the next Z position
        Vector3 spawnPosition = new Vector3(player.position.x, player.position.y, nextSpawnZ);
        Instantiate(roadToSpawn, spawnPosition, Quaternion.identity);

        // Update the next Z position for the next road prefab
        nextSpawnZ += 70f;
    }
}
