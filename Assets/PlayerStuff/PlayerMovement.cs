using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public Rigidbody rb;

    public GameObject streetPrefab; // Reference to the prefab to spawn
    public float spawnInterval = 60f; // Distance the player must travel before spawning a new prefab

    private float totalDistanceTraveled = 0f; // Tracks the total distance traveled by the player
    private Vector3 lastPlayerPosition; // Tracks the last position of the player

    private void Start()
    {
        // Initialize lastPlayerPosition to the player's starting position
        lastPlayerPosition = transform.position;

        // Optionally, spawn an initial prefab at the player's start
        SpawnStreet();
    }

    private void FixedUpdate()
    {
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMove);

        // Calculate the distance traveled since the last frame
        float distanceThisFrame = Vector3.Distance(transform.position, lastPlayerPosition);
        totalDistanceTraveled += distanceThisFrame;

        // Update the last player position
        lastPlayerPosition = transform.position;

        // Check if it's time to spawn a new prefab
        if (totalDistanceTraveled >= spawnInterval)
        {
            SpawnStreet();
            totalDistanceTraveled -= spawnInterval; // Reset the traveled distance counter
        }
    }

    private void SpawnStreet()
    {
        if (streetPrefab != null)
        {
            // Spawn the prefab directly in front of the player along the Z-axis
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + spawnInterval);
            Instantiate(streetPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Street prefab is not assigned in the inspector!");
        }
    }
}
