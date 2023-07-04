using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    public float damageAmount = 10f; // Amount of damage to cause to the player
    public Pawn owner; // Reference to the owner of the object causing damage

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the player's health component
            Health playerHealth = other.GetComponent<Health>();

            if (playerHealth != null && owner != null)
            {
                // Inflict damage on the player
                playerHealth.TakeDamage(damageAmount, owner);
            }
        }
    }
}
