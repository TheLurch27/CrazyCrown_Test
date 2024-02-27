using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image eddingSlotImage;
    public Image crownSlotImage;
    public Image eggSlotImage;
    public Image vomitPowderSlotImage;

    public AudioClip fullInventoryClip;

    private bool itemCollected = false;

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
        if (!IsInventoryFull() && !itemCollected)
        {
            HideAllInventorySlots();

            // Activate the corresponding UI image based on the item tag
            ActivateUIImage(itemTag);

            itemCollected = true;
        }
        else if (IsInventoryFull())
        {
            Debug.LogWarning("Inventory is full!");
            if (fullInventoryClip != null)
            {
                AudioSource.PlayClipAtPoint(fullInventoryClip, transform.position);
            }
        }
    }

    private void ActivateUIImage(string itemTag)
    {
        // Activate the UI image based on the item tag
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
            case "VomitPowder":
                vomitPowderSlotImage.enabled = true;
                break;
            default:
                Debug.LogWarning("Invalid item tag: " + itemTag);
                break;
        }
    }

    public bool IsItemCollected()
    {
        return itemCollected;
    }

    public bool IsInventoryFull()
    {
        return eddingSlotImage.enabled && crownSlotImage.enabled && eggSlotImage.enabled && vomitPowderSlotImage.enabled;
    }

    public void ResetInventory()
    {
        HideAllInventorySlots();
        itemCollected = false;
    }

    public bool IsEddingCollected()
    {
        return eddingSlotImage.enabled;
    }
}
