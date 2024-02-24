using UnityEngine;

public class InteractAndCollect : MonoBehaviour
{
    private bool canInteract = false;
    private Inventory inventory;
    public AudioClip itemAudioClip; // Die Audiodatei für das eingesammelte Item

    private bool itemCollected = false;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>(); // Finde das Inventory-Skript im Spiel
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }

    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E) && !inventory.IsInventoryFull() && !inventory.IsItemCollected())
        {
            CollectItem();
        }
    }

    private void CollectItem()
    {
        // Überprüfe, ob das GameObject einen Collider hat
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            // Entferne das GameObject aus der Szene
            Destroy(gameObject);
            Debug.Log("Gegenstand aufgenommen!");

            // Rufe die Methode AddItemToInventory des Inventar-Skripts auf
            if (inventory != null)
            {
                inventory.AddItemToInventory(gameObject.tag);
            }
            else
            {
                Debug.LogWarning("Inventory-Skript nicht gefunden!");
            }

            itemCollected = true; // Setze die Variable auf true, um zu verhindern, dass die Audiodatei erneut abgespielt wird
        }

        // Spiele die Audiodatei für das eingesammelte Item ab, wenn das Item aufgenommen wurde
        if (itemCollected && itemAudioClip != null)
        {
            AudioSource.PlayClipAtPoint(itemAudioClip, transform.position);
        }
        else
        {
            Debug.LogWarning("Audiodatei nicht zugewiesen oder das Item wurde noch nicht aufgenommen!");
        }
    }
}
