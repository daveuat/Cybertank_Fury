<<<<<<< HEAD
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    public float damageDone; // This variable holds the damage done
    public Pawn owner; // This variable holds the owner of the damage

    public void OnTriggerEnter(Collider other)
    {
        Health otherHealth = other.gameObject.GetComponent<Health>();
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();

        // If the other object has a Health component, then apply damage to it
        if (otherHealth != null)
        {
            // Apply damage based on whether the hit object is a player or enemy
            float damageToApply = damageDone;

            if (owner == null)
            {
                owner = GetComponent<Pawn>();
            }
            otherHealth.TakeDamage(damageToApply, owner);
        }
        // If the other object has an EnemyHealth component, then apply damage to it
        else if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damageDone);
        }

        Destroy(gameObject);
    }

}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    public float damageDone; // Add this line to define the damageDone field
    public Pawn owner; // Add this line to define the owner field

    public void OnTriggerEnter(Collider other)
    {
        // Get the Health component from the Game Object that has the Collider that we are overlapping
        Health otherHealth = other.gameObject.GetComponent<Health>();
        // Only damage if it has a Health component
        if (otherHealth != null)
        {
            // Do damage
            otherHealth.TakeDamage(damageDone, owner);
        }

        // Destroy ourselves, whether we did damage or not
        Destroy(gameObject);
    }
}
>>>>>>> main
