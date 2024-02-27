using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Dictionary zum Speichern des Inventarzustands für jede Szene
    private Dictionary<string, bool> inventoryState = new Dictionary<string, bool>();

    private void Awake()
    {
        // Singleton-Pattern, um sicherzustellen, dass nur eine Instanz des GameManagers existiert
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Das GameManager-Objekt zwischen Szenen nicht zerstören
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Methode zum Hinzufügen eines Items zum Inventarzustand
    public void AddItemToInventoryState(string itemTag, bool isCollected)
    {
        // Überprüfen, ob das Item-Tag bereits im Dictionary existiert
        if (inventoryState.ContainsKey(itemTag))
        {
            inventoryState[itemTag] = isCollected; // Aktualisieren des Zustands des vorhandenen Items
        }
        else
        {
            inventoryState.Add(itemTag, isCollected); // Hinzufügen eines neuen Items zum Dictionary
        }
    }

    // Methode zum Abrufen des Inventarzustands für ein bestimmtes Item
    public bool GetInventoryState(string itemTag)
    {
        // Überprüfen, ob das Item-Tag im Dictionary vorhanden ist
        if (inventoryState.ContainsKey(itemTag))
        {
            return inventoryState[itemTag]; // Rückgabe des Zustands des Items
        }
        else
        {
            return false; // Wenn das Item-Tag nicht vorhanden ist, bedeutet dies, dass das Item nicht eingesammelt wurde
        }
    }

    // Methode zur Überprüfung, ob das Inventar voll ist
    public bool IsInventoryFull()
    {
        // Überprüfen, ob alle Items im Dictionary als eingesammelt markiert sind
        foreach (bool collected in inventoryState.Values)
        {
            if (!collected)
            {
                return false; // Wenn mindestens ein Item nicht eingesammelt ist, ist das Inventar nicht voll
            }
        }
        return true; // Wenn alle Items eingesammelt sind, ist das Inventar voll
    }

    // Methode zum Speichern des aktuellen Inventarzustands beim Laden einer neuen Szene
    public void SaveInventoryState()
    {
        PlayerPrefs.SetInt("InventoryItemCount", inventoryState.Count); // Anzahl der Inventareinträge speichern

        int index = 0;
        foreach (KeyValuePair<string, bool> entry in inventoryState)
        {
            PlayerPrefs.SetString("InventoryItemTag" + index, entry.Key); // Item-Tag speichern
            PlayerPrefs.SetInt("InventoryItemState" + index, entry.Value ? 1 : 0); // Item-Zustand speichern (als 1 für true und 0 für false)

            index++;
        }
        PlayerPrefs.Save(); // PlayerPrefs speichern
    }

    // Methode zum Laden des gespeicherten Inventarzustands beim Starten einer neuen Szene
    public void LoadInventoryState()
    {
        inventoryState.Clear(); // Inventarzustand zurücksetzen

        int itemCount = PlayerPrefs.GetInt("InventoryItemCount", 0); // Anzahl der Inventareinträge laden

        for (int i = 0; i < itemCount; i++)
        {
            string itemTag = PlayerPrefs.GetString("InventoryItemTag" + i); // Item-Tag laden
            bool itemState = PlayerPrefs.GetInt("InventoryItemState" + i) == 1 ? true : false; // Item-Zustand laden und in bool umwandeln

            inventoryState.Add(itemTag, itemState); // Item und Zustand zum Inventarzustand hinzufügen
        }
    }

    // Methode zum Löschen des gespeicherten Inventarzustands (z.B. beim Neustart des Spiels)
    public void ClearInventoryState()
    {
        inventoryState.Clear(); // Inventarzustand zurücksetzen
        PlayerPrefs.DeleteAll(); // PlayerPrefs löschen, um das gespeicherte Inventar zu entfernen
    }
}
