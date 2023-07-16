<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healthAmount = 25f;
    public float powerupDuration = 5f;

    private PowerupManager powerupManager;

    private void Start()
    {
        powerupManager = FindObjectOfType<PowerupManager>();
        if (powerupManager == null)
        {
            Debug.LogError("PowerupManager not found in the scene.");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Pawn pawn = other.GetComponent<Pawn>();
        if (pawn != null)
        {
            HealthPowerup healthPowerup = new HealthPowerup(pawn, healthAmount, powerupDuration);
            powerupManager.AddPowerup(healthPowerup);
            Destroy(gameObject);
        }
    }
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healthAmount = 25f;
    public float powerupDuration = 5f;

    private PowerupManager powerupManager;

    private void Start()
    {
        powerupManager = FindObjectOfType<PowerupManager>();
        if (powerupManager == null)
        {
            Debug.LogError("PowerupManager not found in the scene.");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Pawn pawn = other.GetComponent<Pawn>();
        if (pawn != null)
        {
            HealthPowerup healthPowerup = new HealthPowerup(pawn, healthAmount, powerupDuration);
            powerupManager.AddPowerup(healthPowerup);
            Destroy(gameObject);
        }
    }
>>>>>>> main
}