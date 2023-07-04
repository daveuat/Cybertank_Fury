using UnityEngine;

public class Razorwire : MonoBehaviour
{
    public float damageAmount = 10f; // Adjust the damage amount as needed

    private void OnTriggerEnter(Collider other)
    {
        // Get the Health component from the colliding object
        Health otherHealth = other.GetComponent<Health>();

        // Only damage the player if it has a Health component
        if (otherHealth != null && other.CompareTag("Player"))
        {
            // Do damage to the player
            otherHealth.TakeDamage(damageAmount, null); // Assuming owner is not needed for razorwire
        }
    }
}
