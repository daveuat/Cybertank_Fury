using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class AudioSlider : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;
    [SerializeField]
    private AudioSource AudioSource;
    [SerializeField]
    private TextMeshProUGUI ValueText;
    [SerializeField]
    private AudioMixMode MixMode;

    public void OnChangeSlider(float Value)
    {
        switch (MixMode)
        {
            case AudioMixMode.LinearAudioSourceVolume:
                AudioSource.volume = Value;
                break;
            case AudioMixMode.LinearAudioMixerVolume:
                mixer.SetFloat("Volume", (-80 + Value * 100));
                break;
            case AudioMixMode.LogarithmicAudioMixerVolume:
                mixer.SetFloat("Volume", Mathf.Log10(Value) * 20);
                break;
        }
    }

    public enum AudioMixMode
    {
        LinearAudioSourceVolume,
        LinearAudioMixerVolume,
        LogarithmicAudioMixerVolume
    }

}
