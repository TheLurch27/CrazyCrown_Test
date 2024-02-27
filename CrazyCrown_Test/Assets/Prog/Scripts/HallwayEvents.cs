using UnityEngine;

public class HallwayEvents : MonoBehaviour
{
    [Header("Edding Event")]
    public float interactionTime = 5f; // Zeit, die benötigt wird, um die Interaktion abzuschließen
    public KeyCode[] interactionKeys = { KeyCode.E, KeyCode.UpArrow, KeyCode.Keypad8 }; // Die Tasten, die die Interaktion auslösen
    public AudioClip interactionSound; // Sound, der während der Interaktion abgespielt wird
    public AudioClip completionSound; // Sound, der abgespielt wird, wenn die Interaktion abgeschlossen ist

    [Header("Salute Event")]
    public Collider2D triggerCollider; // Referenz auf den Collider des Empty Game Objects
    public KeyCode[] triggerKeys = { KeyCode.S, KeyCode.DownArrow, KeyCode.Keypad5 }; // Die Tasten, die den Audioclip auslösen sollen
    public AudioClip triggerAudioClip;

    private bool taskCompleted = false;
    private bool interactionInProgress = false;
    private float interactionTimer = 0f;
    private bool playerIsSaluting = false;

    private Inventory inventory;
    private bool playerInEventArea = false;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>(); // Finde das Inventory-Skript im Spiel
        interactionTimer = 0f;
    }

    private void Update()
    {
        if (interactionInProgress)
        {
            interactionTimer -= Time.deltaTime;

            if (interactionTimer <= 0f)
            {
                CompleteTask();
            }
            else if (interactionTimer <= interactionTime && interactionSound != null)
            {
                AudioSource.PlayClipAtPoint(interactionSound, transform.position);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerIsSaluting)
            {
                Debug.Log("Event Abgeschlossen!");
                playerIsSaluting = false;
            }
        }
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
            // Implement any exit actions here if needed
        }
    }

    private void TriggerEvents()
    {
        if (playerInEventArea && IsTriggerKeyPressed())
        {
            Debug.Log("Event Area erreicht und Taste erkannt.");
            if (triggerAudioClip != null)
            {
                AudioSource.PlayClipAtPoint(triggerAudioClip, transform.position);
            }
            // Hier die Methode für den Salut-Event aufrufen
            OnPlayerSaluteEvent();
        }
    }

    private bool IsTriggerKeyPressed()
    {
        foreach (KeyCode key in triggerKeys)
        {
            if (Input.GetKeyDown(key))
            {
                return true;
            }
        }
        return false;
    }

    private void StartInteractionTimer()
    {
        interactionInProgress = true;
        interactionTimer = interactionTime;
    }

    private void ResetInteraction()
    {
        interactionInProgress = false;
    }

    private void CompleteTask()
    {
        taskCompleted = true;
        interactionInProgress = false;
        // Starten der Audioquelle für die Abschlussmeldung
        if (completionSound != null)
        {
            AudioSource.PlayClipAtPoint(completionSound, transform.position);
        }
    }

    private void OnPlayerEnterEventArea()
    {
        // Implement actions to be performed when the player enters the event area
    }

    private void OnPlayerSaluteEvent()
    {
        playerIsSaluting = true;
    }
}
