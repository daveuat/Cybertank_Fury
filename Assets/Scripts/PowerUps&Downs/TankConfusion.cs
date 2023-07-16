using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankConfusion : MonoBehaviour
{
    [SerializeField] private float speedBoost = -10f;
    [SerializeField] private float powerupDuration = 5f;

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
            TankSpeedPowerup speedPowerup = new TankSpeedPowerup(pawn, speedBoost, powerupDuration);
            powerupManager.AddPowerup(speedPowerup);
            Destroy(gameObject);
        }
    }
}
