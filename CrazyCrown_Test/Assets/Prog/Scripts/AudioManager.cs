using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Intro")] // �berschrift f�r den Editorabschnitt
    public float delayInSeconds = 0f; // Zeitverz�gerung in Sekunden
    public AudioClip audioClip; // Audioclip, der abgespielt werden soll

    private AudioSource audioSource;

    private void Start()
    {
        // Audiokomponente finden oder erstellen
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // �berpr�fen, ob ein Audioclip vorhanden ist
        if (audioClip == null)
        {
            Debug.LogError("Audio clip is not assigned in AudioManager!");
            return;
        }

        // Wartezeit abwarten und dann die Audiowiedergabe starten
        Invoke("PlayDelayedAudio", delayInSeconds);
    }

    private void PlayDelayedAudio()
    {
        // Audiowiedergabe starten
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
