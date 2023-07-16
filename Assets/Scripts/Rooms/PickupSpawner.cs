using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [System.Serializable]
    public class RoomSettings
    {
        public GameObject roomPrefab;
        public int pickupsPerRoom;
        public GameObject[] pickupPrefabs;
    }

    public RoomSettings[] roomSettings; // Array of room settings
    public float spawnInterval = 5f; // The time interval between spawns
    public float spawnRadius = 5f; // The radius within which pickups can spawn

    private float timer; // Timer to track spawn intervals

    private void Awake()
    {
        timer = spawnInterval;
    }

    private void Update()
    {
        // Update the timer
        timer -= Time.deltaTime;

        // Check if it's time to spawn a pickup
        if (timer <= 0f)
        {
            SpawnPickup();
            timer = spawnInterval; // Reset the timer
        }
    }

    public void SpawnPickup()
    {
        // Find the current room
        GameObject currentRoom = GetRoomAtPosition(transform.position);

        // Check if the current room is valid
        if (currentRoom == null)
        {
            return; // Exit the method if the current room is not found
        }

        // Get the corresponding room settings for the current room
        RoomSettings currentRoomSettings = GetRoomSettings(currentRoom);

        // Check if the room settings for the current room exist
        if (currentRoomSettings == null)
        {
            return; // Exit the method if the room settings are not found
        }

        // Find all pickups within the spawn radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, spawnRadius, LayerMask.GetMask("Pickup"));

        // Check if the number of pickups within the radius exceeds the maximum limit
        if (colliders.Length >= currentRoomSettings.pickupsPerRoom)
        {
            return; // Exit the method if the maximum limit is reached
        }

        // Calculate a random position within the spawn radius
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        randomPosition.y = transform.position.y; // Make sure the Y position stays the same

        // Select a random pickup prefab from the room settings
        int randomPrefabIndex = Random.Range(0, currentRoomSettings.pickupPrefabs.Length);
        GameObject randomPickupPrefab = currentRoomSettings.pickupPrefabs[randomPrefabIndex];

        Instantiate(randomPickupPrefab, randomPosition, Quaternion.identity);
    }



    private GameObject GetRoomAtPosition(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 1f, LayerMask.GetMask("Room"));
        if (colliders.Length > 0)
        {
            return colliders[0].gameObject;
        }
        return null;
    }

    private RoomSettings GetRoomSettings(GameObject room)
    {
        foreach (RoomSettings settings in roomSettings)
        {
            if (settings.roomPrefab == room)
            {
                return settings;
            }
        }
        return null;
    }
}
