using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Implement any logic you want when the object dies, such as disabling it, triggering an effect, etc.
        gameObject.SetActive(false);
    }
}
