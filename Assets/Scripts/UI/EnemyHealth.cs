using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float startHealth = 100f;
    public Slider healthSlider;
    public Image fillImage;
    public Color fullHealthColor = Color.green;
    public Color zeroHealthColor = Color.red;
    public GameObject[] m_ExplosionPrefabs;
    public AudioClip[] m_ExplosionAudios;
    private AudioSource m_AudioSource;

    private EnemyManager enemyManager;
    private float currentHealth;
    private bool isDestroyed;
    private bool hasDied;

    private void Awake()
    {
        currentHealth = startHealth;
        m_AudioSource = GetComponent<AudioSource>();
        enemyManager = FindObjectOfType<EnemyManager>();
        healthSlider.maxValue = startHealth;
        SetHealthUI();
        hasDied = false;
        fillImage.color = fullHealthColor; // Set the fillImage color to green at start
    }

    public void TakeDamage(float damage)
    {
        // Only apply damage if the enemy isn't already in the process of dying
        if (!isDestroyed)
        {
            currentHealth -= damage;

            // Update the health bar's value and color
            healthSlider.value = currentHealth;
            fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHealth / startHealth);

            if (currentHealth <= 0f)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        isDestroyed = true;

        // Create the explosion prefab at the enemy's position
        int explosionIndex = Random.Range(0, m_ExplosionPrefabs.Length);
        GameObject explosion = Instantiate(m_ExplosionPrefabs[explosionIndex], transform.position, transform.rotation);

        // Play an explosion sound effect
        int audioIndex = Random.Range(0, m_ExplosionAudios.Length);
        m_AudioSource.PlayOneShot(m_ExplosionAudios[audioIndex]);

        Destroy(explosion, 3f);

        Enemy enemyComponent = GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            enemyComponent.OnEnemyDeath();
        }

        enemyManager.EnemyKilled();

        Destroy(gameObject);
    }


    private void SetHealthUI()
    {
        healthSlider.value = currentHealth;
        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHealth / startHealth);
    }
}
