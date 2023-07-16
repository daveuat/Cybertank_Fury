using UnityEngine;

public class Shell : MonoBehaviour
{
    public float damageAmount = 50f; // Adjust as needed

    void OnCollisionEnter(Collision collision)
    {
        var enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damageAmount);
        }

        Destroy(gameObject); // Destroy the shell
    }
}
