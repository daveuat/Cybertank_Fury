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

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    // Copied over from Tanks! script to manage health wheel. some changes made to match my game.
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
    }


    private void OnEnable()
    {
        currentHealth = startHealth;

        SetHealthUI();
    }

    public void TakeDamage(float amount, Pawn source)
    {
        // Adjust the tanks health and update the UI to show the changes.
        currentHealth -= amount;

        // Change UI items
        SetHealthUI();

        // If health 0 then pawn death.
        if (currentHealth <= 0)
        {
            Die(source);
        }
    }

    // Destroy tank with explosion when at 0 health
    public void Die(Pawn source)
    {
        // Plays instantiated explosion with audio at the tank's position
        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);
        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();

        // Destroys Tank
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    // Also from Tanks! and needed for my slider
    public void SetHealthUI()
    {
        // Adjust the value and color of the slider around the tank.
        m_Slider.value = currentHealth;

        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, currentHealth / startHealth);
    }

    public void RestoreHealth(float amount)
    {
        // increase the current health by the specified amount, up to the maximum
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        // update the health UI
        SetHealthUI();
    }

    public float GetCurrentHealthValue()
    {
        return currentHealth;
    }

    public float GetMaxHealthValue()
    {
        return maxHealth;
    }

}
