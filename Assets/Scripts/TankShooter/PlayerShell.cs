using UnityEngine;

public class PlayerShell : MonoBehaviour
{
    public float damageDone;
    public Pawn owner;

    public void OnTriggerEnter(Collider other)
    {
        // Get the Health component from the Game Object that has the Collider that we are overlapping
        Health otherHealth = other.gameObject.GetComponent<Health>();

        // Only damage if it has a Health component and is not the PlayerTank
        if (otherHealth != null && other.gameObject.tag != "Player")
        {
            // Do damage
            otherHealth.TakeDamage(damageDone, owner);
        }

        // Destroy ourselves, whether we did damage or not
        Destroy(gameObject);
    }
}
