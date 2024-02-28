using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusicUI;
    public AudioClip backgroundGame;
    public AudioClip DisclaimerVoice;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayBackgroundMusic(backgroundMusicUI);
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        musicSource.clip = backgroundMusicUI;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySoundEffect()
    {
        musicSource.clip = backgroundGame;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void ChangeMusic(AudioClip newClip)
    {
        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.Play();
    }
}
