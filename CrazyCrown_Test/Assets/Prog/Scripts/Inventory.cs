using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image eddingSlotImage;
    public Image crownSlotImage;
    public Image eggSlotImage;
    public Image vomitPowderSlotImage;

    public AudioClip fullInventoryClip; // Audiokomponente für volles Inventar

    // Variable zur Überprüfung, ob das Inventar voll ist
    private bool inventoryFull = false;
    private bool itemCollected = false; // Variable zur Überprüfung, ob ein Item aufgenommen wurde

    private void Start()
    {
        HideAllInventorySlots();
    }

    private void HideAllInventorySlots()
    {
        eddingSlotImage.enabled = false;
        crownSlotImage.enabled = false;
        eggSlotImage.enabled = false;
        vomitPowderSlotImage.enabled = false;
    }

    public void AddItemToInventory(string itemTag)
    {
        if (!inventoryFull && !itemCollected) // Überprüfe, ob das Inventar nicht voll ist und kein Item aufgenommen wurde
        {
            HideAllInventorySlots();

            switch (itemTag)
            {
                case "Edding":
                    eddingSlotImage.enabled = true;
                    itemCollected = true; // Setze den Status des aufgenommenen Edding-Items auf true
                    break;
                case "Crown":
                    crownSlotImage.enabled = true;
                    itemCollected = true; // Setze den Status des aufgenommenen Crown-Items auf true
                    break;
                case "Egg":
                    eggSlotImage.enabled = true;
                    itemCollected = true; // Setze den Status des aufgenommenen Egg-Items auf true
                    break;
                case "Vomit-Powder":
                    vomitPowderSlotImage.enabled = true;
                    itemCollected = true; // Setze den Status des aufgenommenen Vomit-Powder-Items auf true
                    break;
                default:
                    Debug.LogWarning("Unrecognized item tag: " + itemTag);
                    break;
            }
        }
        else if (inventoryFull)
        {
            Debug.LogWarning("Inventory is full!");
            if (fullInventoryClip != null) // Überprüfe, ob die Audiokomponente vorhanden ist
            {
                AudioSource.PlayClipAtPoint(fullInventoryClip, transform.position); // Spiele die Audioquelle ab, wenn das Inventar voll ist
            }
        }
    }

    // Methode zum Setzen des Inventarstatus auf voll
    public void SetInventoryFull(bool isFull)
    {
        inventoryFull = isFull;
    }

    // Methode zur Überprüfung, ob das Inventar voll ist
    public bool IsInventoryFull()
    {
        return eddingSlotImage.enabled && crownSlotImage.enabled && eggSlotImage.enabled && vomitPowderSlotImage.enabled;
    }

    public bool IsItemCollected()
    {
        return itemCollected;
    }

    // Methode zum Zurücksetzen des Inventars
    public void ResetInventory()
    {
        HideAllInventorySlots();
        itemCollected = false;
    }

    public void CollectItem(string itemTag)
    {
        if (!inventoryFull && !itemCollected) // Überprüfe, ob das Inventar nicht voll ist und kein Item aufgenommen wurde
        {
            HideAllInventorySlots();

            switch (itemTag)
            {
                case "Edding":
                    eddingSlotImage.enabled = true;
                    itemCollected = true; // Setze den Status des aufgenommenen Edding-Items auf true
                    break;
                case "Crown":
                    crownSlotImage.enabled = true;
                    itemCollected = true; // Setze den Status des aufgenommenen Crown-Items auf true
                    break;
                case "Egg":
                    eggSlotImage.enabled = true;
                    itemCollected = true; // Setze den Status des aufgenommenen Egg-Items auf true
                    break;
                case "Vomit-Powder":
                    vomitPowderSlotImage.enabled = true;
                    itemCollected = true; // Setze den Status des aufgenommenen Vomit-Powder-Items auf true
                    break;
                default:
                    Debug.LogWarning("Unrecognized item tag: " + itemTag);
                    break;
            }
        }
        else if (inventoryFull)
        {
            Debug.LogWarning("Inventory is full!");
            if (fullInventoryClip != null) // Überprüfe, ob die Audiokomponente vorhanden ist
            {
                AudioSource.PlayClipAtPoint(fullInventoryClip, transform.position); // Spiele die Audioquelle ab, wenn versucht wird, ein weiteres Item in das volle Inventar zu nehmen
            }
        }
    }

    public bool IsEddingCollected()
    {
        return eddingSlotImage.enabled;
    }
}
