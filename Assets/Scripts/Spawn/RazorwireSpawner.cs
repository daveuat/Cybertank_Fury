using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazorwireSpawner : MonoBehaviour
{
    public GameObject razorwirePrefab; // Prefab of the razorwire to spawn
    public Transform[] spawnPoints; // Array of spawn points
    public string entranceTag = "Entrance"; // Tag for detecting "Entrance" objects
    public float checkBoxSize = 1f; // Size of the checkbox for overlap detection
    [Range(0f, 1f)]
    public float spawnPercentage = 0.34f; // Percentage chance of spawning razorwire

    private void Start()
    {
        SpawnRazorwire();
    }

    private void SpawnRazorwire()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform spawnPoint = spawnPoints[i];
            Vector3 checkBoxCenter = spawnPoint.position;
            Vector3 checkBoxHalfExtents = new Vector3(checkBoxSize / 2f, checkBoxSize / 2f, checkBoxSize / 2f);

            Collider[] colliders = Physics.OverlapBox(checkBoxCenter, checkBoxHalfExtents, Quaternion.identity);

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag(entranceTag))
                {
                    // Randomly determine if razorwire should be spawned
                    bool spawnRazorwire = Random.value < spawnPercentage;

                    if (spawnRazorwire)
                    {
                        // Entrance detected at spawn point, spawn the razorwire
                        GameObject razorwire = Instantiate(razorwirePrefab, spawnPoint.position, Quaternion.identity);
                        // Set the razorwire's parent to this object for organization
                        razorwire.transform.SetParent(transform);
                    }

                    break; // Stop checking for entrances at this spawn point
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (Transform spawnPoint in spawnPoints)
        {
            Vector3 checkBoxCenter = spawnPoint.position;
            Vector3 checkBoxHalfExtents = new Vector3(checkBoxSize / 2f, checkBoxSize / 2f, checkBoxSize / 2f);
            Gizmos.DrawWireCube(checkBoxCenter, checkBoxHalfExtents * 2f);
        }
    }
}
