using UnityEngine;

public class HallwayEvents : MonoBehaviour
{
    private bool taskCompleted = false;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop(); // Stellt sicher, dass die Audiospur zu Beginn gestoppt ist
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Event Area erreicht");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Event Area verlassen");
        }
    }

    private void Update()
    {
        if (!taskCompleted && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Keypad5)))
        {
            Debug.Log("Aufgabe bestanden");
            taskCompleted = true;
            PlayAudio(); // Spielt die Audiospur ab, wenn die Aufgabe bestanden wurde
        }
    }

    private void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
