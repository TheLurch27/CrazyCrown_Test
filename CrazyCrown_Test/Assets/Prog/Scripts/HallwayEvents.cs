using UnityEngine;
using UnityEngine.Audio;

public class HallwayEvents : MonoBehaviour
{
    [Header("•••••••••••Salute Event•••••••••••")]
    [SerializeField] private AudioClip butlerMessage;
    [SerializeField] private Collider2D triggerPlace;

    // public void AssignTrigger(Collider2D collider)
    // {
    //     triggerPlace = collider;
    // }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == triggerPlace && collision.CompareTag("Player"))
        {
            Debug.Log("In der Area");

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Keypad5))
            {
                Debug.Log("Salute Event ausgelöst!");

                if (butlerMessage != null)
                {
                    // Leer
                }
                else
                {
                    Debug.LogWarning("Audio Clip nicht zugewiesen!");
                }
            }
        }
    }

    private void Start()
    {
        Collider2D assignedCollider = GetComponent<Collider2D>();
        if (assignedCollider != null)
        {
            // AssignTrigger(assignedCollider);
        }
        else
        {
            Debug.LogWarning("Collider nicht gefunden! Bitte überprüfe, ob ein Collider dem GameObject zugewiesen ist.");
        }
    }
}

