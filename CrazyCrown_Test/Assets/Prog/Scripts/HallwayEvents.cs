using UnityEngine;
using UnityEngine.UI;

public class HallwayEvents : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionTime = 5f; // Zeit, die benötigt wird, um die Interaktion abzuschließen
    public KeyCode[] interactionKeys = { KeyCode.E, KeyCode.UpArrow, KeyCode.Keypad8 }; // Die Tasten, die die Interaktion auslösen

    [Header("Game Objects")]
    public GameObject portraitSmear; // Referenz auf das GameObject, das eingblendet werden soll
    public GameObject portraitNormal; // Referenz auf das GameObject, das ausgeblendet werden soll

    [Header("Audio")]
    public AudioClip interactionSound; // Sound, der während der Interaktion abgespielt wird
    public AudioClip completionSound; // Sound, der abgespielt wird, wenn die Interaktion abgeschlossen ist

    private bool taskCompleted = false;
    private bool interactionInProgress = false;
    private float interactionTimer = 0f;

    private InGameUI inGameUI; // Referenz auf das InGameUI-Skript
    private Inventory inventory; // Referenz auf das Inventory-Skript

    private void Start()
    {
        inGameUI = FindObjectOfType<InGameUI>(); // Finde das InGameUI-Skript im Spiel
        inventory = FindObjectOfType<Inventory>(); // Finde das Inventory-Skript im Spiel
        interactionTimer = 0f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!taskCompleted && IsInteractionKeyPressed() && inventory.IsEddingCollected()) // Überprüfe, ob der Edding im Inventar ist und die richtige Taste gedrückt wurde
            {
                StartInteractionTimer();
            }
            else
            {
                ResetInteraction();
            }
        }
    }

    private bool IsInteractionKeyPressed()
    {
        foreach (KeyCode key in interactionKeys)
        {
            if (Input.GetKey(key))
            {
                return true;
            }
        }
        return false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ResetInteraction();
        }
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
                // Spiele den Interaktions-Sound ab
                AudioSource.PlayClipAtPoint(interactionSound, transform.position);
            }

            inGameUI.UpdateInteractionSlider(interactionTimer); // Aktualisiere den Slider
        }
    }

    private void StartInteractionTimer()
    {
        interactionInProgress = true;
        interactionTimer = interactionTime;
        inGameUI.ShowInteractionSlider(interactionTime); // Zeige den Slider an und setze die Interaktionszeit
    }

    private void ResetInteraction()
    {
        interactionInProgress = false;
        inGameUI.HideInteractionSlider(); // Verberge den Slider
    }

    private void CompleteTask()
    {
        taskCompleted = true;
        interactionInProgress = false;

        // Einblenden des Portrait_smear GameObjects
        portraitSmear.SetActive(true);

        // Ausblenden des Portrait_normal GameObjects
        portraitNormal.SetActive(false);

        // Starten der Audioquelle für die Abschlussmeldung
        if (completionSound != null)
        {
            AudioSource.PlayClipAtPoint(completionSound, transform.position);
        }

        // Verberge den Slider
        inGameUI.HideInteractionSlider();
    }
}
