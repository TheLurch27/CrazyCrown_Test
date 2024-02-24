using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image eddingSlotImage;
    public Image crownSlotImage;
    public Image eggSlotImage;
    public Image vomitPowderSlotImage;

    public AudioSource fullInventoryAudio; // Audiokomponente für volles Inventar

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
                    break;
                case "Crown":
                    crownSlotImage.enabled = true;
                    break;
                case "Egg":
                    eggSlotImage.enabled = true;
                    break;
                case "Vomit-Powder":
                    vomitPowderSlotImage.enabled = true;
                    break;
                default:
                    Debug.LogWarning("Unrecognized item tag: " + itemTag);
                    break;
            }

            itemCollected = true; // Setze den Status des aufgenommenen Items auf true
        }
        else if (inventoryFull)
        {
            Debug.LogWarning("Inventory is full!");
            if (fullInventoryAudio != null) // Überprüfe, ob die Audiokomponente vorhanden ist
            {
                fullInventoryAudio.Play(); // Spiele die Audioquelle ab, wenn das Inventar voll ist
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
        return inventoryFull;
    }
}
