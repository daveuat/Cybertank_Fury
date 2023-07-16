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
