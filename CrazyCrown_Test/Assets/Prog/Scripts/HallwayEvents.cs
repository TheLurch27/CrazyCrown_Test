using UnityEngine;
using UnityEngine.UI;

public class HallwayEvents : MonoBehaviour
{
    private bool taskCompleted = false;
    private bool interactionInProgress = false;
    private float interactionTimer = 0f;

    public float interactionTime = 5f; // Zeit, die benötigt wird, um die Interaktion abzuschließen

    public GameObject portraitSmear; // Referenz auf das GameObject, das eingblendet werden soll
    public GameObject portraitNormal; // Referenz auf das GameObject, das ausgeblendet werden soll

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
            if (!taskCompleted && Input.GetKeyDown(KeyCode.E) && inventory.IsEddingCollected()) // Überprüfe, ob der Edding im Inventar ist
            {
                Debug.Log("Interaktion gestartet");
                interactionInProgress = true;
                inGameUI.ShowInteractionSlider(interactionTime); // Zeige den Slider an und setze die Interaktionszeit
            }
        }
    }

    private void Update()
    {
        if (interactionInProgress)
        {
            interactionTimer += Time.deltaTime;

            if (interactionTimer >= interactionTime)
            {
                Debug.Log("Trigger abgeschlossen");
                CompleteTask();
            }
        }
    }

    private void CompleteTask()
    {
        taskCompleted = true;
        interactionInProgress = false;

        // Einblenden des Portrait_smear GameObjects
        portraitSmear.SetActive(true);

        // Ausblenden des Portrait_normal GameObjects
        portraitNormal.SetActive(false);
    }
}
