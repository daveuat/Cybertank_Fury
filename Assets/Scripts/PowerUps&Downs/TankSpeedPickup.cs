using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpeedPickup : MonoBehaviour
{
    [SerializeField] private float speedBoost = 10f;
    [SerializeField] private float powerupDuration = 5f;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private float destroyDelay = 0.5f;

    private PowerupManager powerupManager;
    private AudioSource audioSource;

    private void Start()
    {
        powerupManager = FindObjectOfType<PowerupManager>();
        if (powerupManager == null)
        {
            Debug.LogError("PowerupManager not found in the scene.");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Add an AudioSource component if not already present
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Pawn pawn = other.GetComponent<Pawn>();
        if (pawn != null)
        {
            TankSpeedPowerup speedPowerup = new TankSpeedPowerup(pawn, speedBoost, powerupDuration);
            powerupManager.AddPowerup(speedPowerup);

            if (pickupSound != null)
            {
                // Play the pickup sound
                audioSource.PlayOneShot(pickupSound);
            }

            // Delay the destruction of the object
            StartCoroutine(DestroyWithDelay());
        }
    }

    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
