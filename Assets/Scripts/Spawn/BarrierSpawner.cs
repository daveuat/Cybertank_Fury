using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierSpawner : MonoBehaviour
{
    public GameObject barrierPrefab; // Prefab of the barrier to spawn
    public Transform[] spawnPoints; // Array of spawn points
    public string entranceTag = "Entrance"; // Tag for detecting "Entrance" objects
    public float checkBoxSize = 1f; // Size of the checkbox for overlap detection
    [Range(0f, 1f)]
    public float spawnPercentage = 0.34f; // Percentage chance of spawning a barrier

    private void Start()
    {
        SpawnBarriers();
    }

    private void SpawnBarriers()
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
                    // Randomly determine if a barrier should be spawned
                    bool spawnBarrier = Random.value < spawnPercentage;

                    if (spawnBarrier)
                    {
                        // Entrance detected at spawn point, spawn the barrier
                        GameObject barrier = Instantiate(barrierPrefab, spawnPoint.position, spawnPoint.rotation);
                        // Set the barrier's parent to this object for organization
                        barrier.transform.SetParent(transform);
                    }

                    break; // Stop checking for entrances at this spawn point
                }
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        foreach (Transform spawnPoint in spawnPoints)
        {
            Vector3 checkBoxCenter = spawnPoint.position;
            Vector3 checkBoxHalfExtents = new Vector3(checkBoxSize / 2f, checkBoxSize / 2f, checkBoxSize / 2f);
            Gizmos.DrawWireCube(checkBoxCenter, checkBoxHalfExtents * 2f);
        }
    }
}
