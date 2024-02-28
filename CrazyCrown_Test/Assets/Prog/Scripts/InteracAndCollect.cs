using UnityEngine;

public class InteractAndCollect : MonoBehaviour
{
    private bool canInteract = false;
    private Inventory inventory;
    public AudioClip itemAudioClip;

    private bool itemCollected = false;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
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
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            
            Destroy(gameObject);
            Debug.Log("Gegenstand aufgenommen!");

            
            if (inventory != null)
            {
                inventory.AddItemToInventory(gameObject.tag);
            }
            else
            {
                Debug.LogWarning("Inventory-Skript nicht gefunden!");
            }

            itemCollected = true;
        }

        
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
