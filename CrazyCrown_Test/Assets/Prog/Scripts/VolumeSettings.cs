using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    // Referenzen auf den AudioMixer und die Schieberegler für die Lautstärkeregelung
    [SerializeField] private AudioMixer MainMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start()
    {
        // Überprüfen, ob Lautstärkeeinstellungen gespeichert sind
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            // Wenn gespeichert, Lautstärke laden
            LoadVolume();
        }
        else
        {
            // Ansonsten Standardwerte setzen
            SetMasterVolume();
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    // Methode zum Setzen der Masterlautstärke
    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        MainMixer.SetFloat("Master", Mathf.Log10(volume) * 20); // Lautstärke im Mixer setzen
        PlayerPrefs.SetFloat("masterVolume", volume); // Lautstärke speichern
    }

    // Methode zum Setzen der Musiklautstärke
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        MainMixer.SetFloat("Music", Mathf.Log10(volume) * 20); // Lautstärke im Mixer setzen
        PlayerPrefs.SetFloat("musicVolume", volume); // Lautstärke speichern
    }

    // Methode zum Setzen der SFX-Lautstärke
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        MainMixer.SetFloat("SFX", Mathf.Log10(volume) * 20); // Lautstärke im Mixer setzen
        PlayerPrefs.SetFloat("SFXVolume", volume); // Lautstärke speichern
    }

    // Methode zum Laden der gespeicherten Lautstärkeeinstellungen
    private void LoadVolume()
    {
        // Gespeicherte Lautstärkewerte laden
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        // Lautstärke entsprechend der geladenen Werte setzen
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }
}