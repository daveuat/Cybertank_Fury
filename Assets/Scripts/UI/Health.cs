using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    private RandomSoundOnDamage randomSoundOnDamage;

    public LivesManager livesManager; // Reference to the LivesManager

    // Health fields
    public float currentHealth = 100f;
    public float maxHealth = 100f;
    public float startHealth = 100f;
    //public int maxLives = 3;
    //public int currentLives = 3;

    // Armor Implementation
    public bool hasArmor;
    private int armorUpgradeCount;
    public Color normalHealthColor = Color.green;
    public Color armorHealthColor = Color.blue;

    // Copied over from Tanks! script to manage health wheel. Some changes made to match my game.
    public Slider m_Slider;
    public Image m_FillImage;
    public Color m_FullHealthColor = Color.green;
    public Color m_ZeroHealthColor = Color.red;
    public GameObject[] m_ExplosionPrefabs;
    public AudioClip[] m_ExplosionAudios;

    // On-hit sounds
    public AudioClip[] hitSounds;

    private bool hasExploded = false;
    private bool isDestroyed = false;
    private bool isDying = false;
    private bool isInvulnerable = false;

    private AudioSource m_AudioSource;

    private GameManager gameManager;


    private void Start()
    {
        currentHealth = startHealth;
    }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        // Initialize armor status
        hasArmor = false;
        armorUpgradeCount = 0;

        // Set initial health bar color
        SetHealthBarColor(normalHealthColor);

        SetHealthUI();

        // Get the AudioSource component
        m_AudioSource = GetComponent<AudioSource>();
        if (m_AudioSource == null)
        {
            // Add AudioSource component if not already present
            m_AudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnEnable()
    {
        currentHealth = startHealth;
        SetInvulnerability(3f);
        SetHealthUI();
    }

    public void TakeDamage(float amount, Pawn source)
    {
        if (isInvulnerable) // Ignore damage if invulnerable
            return;
        if (hasArmor)
        {
            if (isInvulnerable)
            {
                return;
            }
            if (amount <= currentHealth)
            {
                currentHealth -= amount;
            }
            else
            {
                float remainingDamage = amount - currentHealth;
                currentHealth = 0;
                hasArmor = false;
                SetHealthBarColor(normalHealthColor);
            }
        }
        else
        {
            currentHealth -= amount;
        }

        SetHealthUI();

        if (currentHealth <= 0 && !isDying)
        {
            isDying = true;
            Die(source);
        }
        else
        {
            PlayHitSound();
        }
    }

    public void SetInvulnerability(float duration)
    {
        StartCoroutine(SetInvulnerabilityCoroutine(duration));
    }

    private IEnumerator SetInvulnerabilityCoroutine(float duration)
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(duration);
        isInvulnerable = false;
    }

    public void Die(Pawn source)
    {
        GameManager.instance.PlayerDied();

        StartCoroutine(DieWhenReady(source));
    }


    private IEnumerator DieWhenReady(Pawn source)
    {
        yield return new WaitUntil(() => GameManager.instance != null);

        if (currentHealth <= 0 && !isDying)
        {
            isDying = true;

            if (gameObject.CompareTag("Player"))
            {


                // Add armor checks
                Health playerHealth = GetComponent<Health>();
                if (playerHealth != null && playerHealth.hasArmor)
                {
                    playerHealth.hasArmor = false;
                    playerHealth.SetHealthBarColor(playerHealth.normalHealthColor);
                    playerHealth.RestoreHealth(playerHealth.maxHealth);
                    isDying = false;
                    yield break;
                }

                GameManager.instance.PlayerDied();

                yield return new WaitUntil(() => GameManager.instance.isRespawning == false);


            }

            RestoreHealth(maxHealth);
            isDying = false;
        }
    }



    public void RestoreHealth(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
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

    public void SetHealthUI()
    {
        m_Slider.value = currentHealth;

        if (hasArmor)
        {
            m_FillImage.color = armorHealthColor;
        }
        else
        {
            m_FillImage.color = m_FullHealthColor;
        }
    }

    public void ApplyArmorUpgrade(float amount)
    {
        maxHealth += amount;
        currentHealth += amount;
        hasArmor = true;
        armorUpgradeCount++;

        SetHealthUI();
        if (!hasArmor)
        {
            SetHealthBarColor(normalHealthColor);
        }
    }

    public void SetHealthBarColor(Color color)
    {
        m_FillImage.color = color;
    }

    private void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0 && !isDestroyed)
        {
            Die(null);
        }
    }

    private void PlayHitSound()
    {
        if (hitSounds != null && hitSounds.Length > 0)
        {
            int index = Random.Range(0, hitSounds.Length);
            AudioClip hitSound = hitSounds[index];
            m_AudioSource.PlayOneShot(hitSound);
        }
    }
}
