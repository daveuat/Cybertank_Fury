using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazorSpawner : MonoBehaviour
{
    public GameObject razorPrefab; // Prefab of the razorwire to spawn
    public Transform[] spawnPoints; // Array of spawn points

    private void Start()
    {
        SpawnRazorwire();
    }

    private void SpawnRazorwire()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform spawnPoint = spawnPoints[i];

            // Randomly determine if a razorwire should be spawned
            bool spawnRazorwire = Random.value < 0.34f; // 34% chance

            if (spawnRazorwire)
            {
                // Spawn the razorwire at the spawn point
                GameObject razorwire = Instantiate(razorPrefab, spawnPoint.position, Quaternion.identity);
                // Rotate the razorwire 90 degrees around the Y-axis
                razorwire.transform.Rotate(Vector3.up, 90f);
                // Set the razorwire's parent to this object for organization
                razorwire.transform.SetParent(transform);
            }
        }
    }

}
