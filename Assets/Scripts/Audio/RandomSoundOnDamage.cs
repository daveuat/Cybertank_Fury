using UnityEngine;

public class RandomSoundOnDamage : MonoBehaviour
{
    public AudioClip[] damageSounds;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayRandomDamageSound()
    {
        if (damageSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, damageSounds.Length);
            audioSource.PlayOneShot(damageSounds[randomIndex]);
        }
    }
}
