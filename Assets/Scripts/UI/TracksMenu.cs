using UnityEngine;
using TMPro;

public class TracksMenu : MonoBehaviour
{
    public SoundManager soundManager;
    public TMP_Dropdown musicTrackDropdown;

    private void Start()
    {
        PopulateMusicTrackDropdown();
        LoadOptions();
    }

    private void PopulateMusicTrackDropdown()
    {
        musicTrackDropdown.ClearOptions();

        // Create a list of music track names
        var trackNames = new System.Collections.Generic.List<string>();
        foreach (var track in soundManager.musicTracks)
        {
            trackNames.Add(track.name);
        }

        // Populate the dropdown with the track names
        musicTrackDropdown.AddOptions(trackNames);
    }

    public void OnMusicTrackSelectionChanged()
    {
        if (soundManager != null && soundManager.musicSource != null) // Add null checks
        {
            int selectedIndex = musicTrackDropdown.value;
            string selectedTrackName = musicTrackDropdown.options[selectedIndex].text;
            soundManager.PlayMusicByTrackName(selectedTrackName);
        }
    }

    private void LoadOptions()
    {
        if (soundManager != null && soundManager.musicSource != null) // Add null check
        {
            // Get the index of the currently playing music track
            int currentTrackIndex = -1;
            for (int i = 0; i < soundManager.musicTracks.Length; i++)
            {
                if (soundManager.musicTracks[i].clip == soundManager.musicSource.clip)
                {
                    currentTrackIndex = i;
                    break;
                }
            }

            // Set the dropdown value to the index of the current track
            musicTrackDropdown.SetValueWithoutNotify(currentTrackIndex);
        }
    }
}
