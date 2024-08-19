using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private AudioMixer _audioMixer;

    private void Start()
    {
        Initialize();
        _soundSlider.onValueChanged.AddListener(SetSoundVolume);
        _musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    private void Initialize()
    {
        _soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 1f);
        _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);

        SetSoundVolume(_soundSlider.value);
        SetMusicVolume(_musicSlider.value);
    }

    private void SetSoundVolume(float volume)
    {
        _audioMixer.SetFloat("SoundVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SoundVolume", volume);
        PlayerPrefs.Save();
    }

    private void SetMusicVolume(float volume)
    {
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
}
