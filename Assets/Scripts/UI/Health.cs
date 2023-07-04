using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // Health fields
    public float currentHealth;
    public float maxHealth;
    public float startHealth;

    // Armor Implementation
    private bool hasArmor;
    private int armorUpgradeCount;
    private Color normalHealthColor = Color.green;
    private Color armorHealthColor = Color.blue;

    // Copied over from Tanks! script to manage health wheel. Some changes made to match my game.
    public Slider m_Slider;
    public Image m_FillImage;
    public Color m_FullHealthColor = Color.green;
    public Color m_ZeroHealthColor = Color.red;
    public GameObject m_ExplosionPrefab;

    private AudioSource m_ExplosionAudio;
    private ParticleSystem m_ExplosionParticles;

    private void Awake()
    {
        // Instantiate the explosion prefab from my assets
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        // Set a reference to an audio source for the above prefab
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();
        // Disables the prefab until it is activated
        m_ExplosionParticles.gameObject.SetActive(false);

        // Initialize armor status
        hasArmor = false;
        armorUpgradeCount = 0;

        // Set initial health bar color
        SetHealthBarColor(normalHealthColor);

        SetHealthUI();
    }

    private void OnEnable()
    {
        currentHealth = startHealth;

        SetHealthUI();
    }

    public void TakeDamage(float amount, Pawn source)
    {
        if (hasArmor)
        {
            // Check if the armor health is enough to absorb the damage
            if (amount <= currentHealth)
            {
                // Reduce the armor health
                currentHealth -= amount;
            }
            else
            {
                // Calculate the remaining damage after the armor is depleted
                float remainingDamage = amount - currentHealth;

                // Deplete the armor health
                currentHealth = 0;
                hasArmor = false;

                // Set the health bar color to green when the armor is depleted
                SetHealthBarColor(normalHealthColor);
            }
        }
        else
        {
            // Adjust the tank's health and update the UI to show the changes.
            currentHealth -= amount;
        }

        // Change UI items
        SetHealthUI();

        // If health is 0, the pawn dies.
        if (currentHealth <= 0)
        {
            Die(source);
        }
    }








    public void Die(Pawn source)
    {
        if (hasArmor)
        {
            hasArmor = false;
            SetHealthBarColor(normalHealthColor);
            return;
        }

        // Plays instantiated explosion with audio at the tank's position
        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);
        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();

        // Destroys Tank
        Destroy(gameObject);
    }

    public void RestoreHealth(float amount)
    {
        // Increase the current health by the specified amount, up to the maximum
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        // Update the health UI
        SetHealthUI();
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetCurrentHealthValue()
    {
        return currentHealth;
    }

    public float GetMaxHealthValue()
    {
        return maxHealth;
    }

    public int GetArmorUpgradeCount()
    {
        return armorUpgradeCount;
    }

    private void SetHealthUI()
    {
        // Adjust the value and color of the slider around the tank.
        m_Slider.value = currentHealth;

        float normalizedHealth = currentHealth / maxHealth;

        // Update health bar color based on armor status
        if (hasArmor)
        {
            m_FillImage.color = armorHealthColor;
        }
        else if (m_FillImage.color != normalHealthColor)
        {
            m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, normalizedHealth);
        }
    }






    public void ApplyArmorUpgrade(float amount)
    {
        maxHealth += amount;
        currentHealth += amount;
        hasArmor = true;
        armorUpgradeCount++;

        // Update health UI and health bar color
        SetHealthUI();
        if (!hasArmor)
        {
            SetHealthBarColor(normalHealthColor);
        }
    }





    private void SetHealthBarColor(Color color)
    {
        m_FillImage.color = color;
    }

    private void Start()
    {
        currentHealth = startHealth;
    }

    private void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
}
