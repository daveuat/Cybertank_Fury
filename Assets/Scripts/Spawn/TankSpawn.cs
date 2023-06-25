using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawn : MonoBehaviour
{
    public GameObject tankPrefab; // The prefab of the tank to spawn
    public Transform spawnPoint; // The spawn point object
    public float spawnInterval = 5f; // The time between spawns

    private GameObject currentTank; // The current tank in the scene
    private float nextSpawnTime; // The time to spawn the next tank

    // Start is called before the first frame update
    void Start()
    {
        // Call the SpawnTank method to spawn the tank when the game starts or whenever you want
        SpawnTank();
    }

    public void SpawnTank()
    {
        currentTank = Instantiate(tankPrefab, spawnPoint.position, spawnPoint.rotation);
        // Add any additional setup or customization for the spawned tank if needed
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTank == null && Time.time >= nextSpawnTime)
        {
            SpawnTank();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }
}
