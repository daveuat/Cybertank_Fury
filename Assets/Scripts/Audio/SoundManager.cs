using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class MusicTrack
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume = 1f;
    }

    public MusicTrack[] musicTracks;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public string musicVolumeParameter = "MusicVolume";
    public string sfxVolumeParameter = "SFXVolume";

    [Range(0f, 1f)]
    public float musicVolume = 1f;

    [Range(0f, 1f)]
    public float sfxVolume = 1f;

    private float defaultMusicVolume; // Store the default music volume

    private void Awake()
    {
        defaultMusicVolume = musicVolume; // Store the default music volume

        LoadSettings(); // Load the saved settings

        DontDestroyOnLoad(gameObject);

    }

    private void OnDisable()
    {
        SaveSettings();
    }

    private MusicTrack FindMusicTrackByName(string name)
    {
        return System.Array.Find(musicTracks, track => track.name == name);
    }

    public void PlayMusicByTrackName(string trackName)
    {
        MusicTrack track = FindMusicTrackByName(trackName);
        if (track != null)
        {
            if (musicSource.isPlaying)
            {
                musicSource.Stop();
            }
            musicSource.clip = track.clip;
            musicSource.volume = track.volume * musicVolume;
            musicSource.Play();
        }
    }

    public void SetMusicTrack(AudioClip musicClip, float volume)
    {
        musicSource.clip = musicClip;
        musicSource.volume = volume * musicVolume;
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        ApplyVolumeSettings();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        ApplyVolumeSettings();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    public void PlayMusic(string sceneName)
    {
        MusicTrack track = FindMusicTrackByName(sceneName);
        if (track != null)
        {
            if (musicSource.isPlaying)
            {
                musicSource.Stop();
            }
            musicSource.clip = track.clip;
            musicSource.volume = track.volume * musicVolume;
            musicSource.Play();
        }
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", defaultMusicVolume);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        ApplyVolumeSettings();
    }

    public void PlayMusicByTrackIndex(int trackIndex)
    {
        if (trackIndex >= 0 && trackIndex < musicTracks.Length)
        {
            if (musicSource.isPlaying)
            {
                musicSource.Stop();
            }
            musicSource.clip = musicTracks[trackIndex].clip;
            musicSource.volume = musicTracks[trackIndex].volume * musicVolume;
            musicSource.Play();
        }
    }

    private void ApplyVolumeSettings()
    {
        musicVolume = Mathf.Clamp01(musicVolume);
        sfxVolume = Mathf.Clamp01(sfxVolume);

        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;

        AudioMixer audioMixer = musicSource.outputAudioMixerGroup.audioMixer;

        audioMixer.SetFloat(musicVolumeParameter, ConvertVolumeToMixerValue(musicVolume));
        audioMixer.SetFloat(sfxVolumeParameter, ConvertVolumeToMixerValue(sfxVolume));

        if (musicVolume == 0f)
        {
            musicSource.Pause();
        }
        else if (!musicSource.isPlaying)
        {
            musicSource.UnPause();
        }
    }

    private float ConvertVolumeToMixerValue(float volume)
    {
        float mixerValue = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20f;
        return Mathf.Clamp(mixerValue, -80f, 0f);
    }
}
