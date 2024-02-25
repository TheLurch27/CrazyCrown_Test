using UnityEngine;

public class LinnetController : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5f;
    public AudioClip alertSound;

    private AudioSource audioSource;
    private PlayerController playerController; // Referenz auf das PlayerController-Skript
    private bool isPlayerDetected = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = player.GetComponent<PlayerController>(); // Spielercharakter holen und PlayerController-Skript erhalten
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= detectionRadius)
        {
            // Hier überprüfen wir direkt den Schleichzustand des Spielers
            if (!playerController.IsSneaking) // Beachte, dass wir nicht mehr IsSneaking() aufrufen, sondern direkt auf die Variable zugreifen
            {
                if (!isPlayerDetected)
                {
                    isPlayerDetected = true;
                    PlayAlertSound();
                }
            }
            else
            {
                isPlayerDetected = false;
            }
        }
        else
        {
            isPlayerDetected = false;
        }
    }

    void PlayAlertSound()
    {
        if (alertSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(alertSound);
        }
    }
}