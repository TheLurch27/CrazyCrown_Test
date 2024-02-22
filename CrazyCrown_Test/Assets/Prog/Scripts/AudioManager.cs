using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Intro")]
    public float delayInSeconds = 0f;
    public AudioClip audioClip;

    [Header("WalkieTalkie Message #01")]
    public float deslayInSeconds = 0f;
    public AudioClip audioClip1;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (audioClip == null)
        {
            return;
        }

        Invoke("PlayDelayedAudio", delayInSeconds);
    }

    private void PlayDelayedAudio()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
