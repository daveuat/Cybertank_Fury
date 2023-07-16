using UnityEngine;

public class FireSounds : MonoBehaviour
{
    public AudioClip[] fireSounds;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (fireSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, fireSounds.Length);
            audioSource.clip = fireSounds[randomIndex];
            audioSource.Play();
        }
    }
}
