using UnityEngine;

public class InteractAndCollect : MonoBehaviour
{
    private bool canInteract = false;

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
        if (canInteract && Input.GetKeyDown(KeyCode.E))
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
        }
    }
}
