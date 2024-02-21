using UnityEngine;

public class EventManager : MonoBehaviour
{
    public AudioSource messageLaterAudio; // Verweisen Sie hier im Editor auf die "Message Later" Audioquelle

    public void SalutePointReached()
    {
        // Überprüfen Sie, ob die "Message Later" Audioquelle vorhanden ist
        if (messageLaterAudio != null)
        {
            // Starten Sie die Audioquelle
            messageLaterAudio.Play();
        }
        else
        {
            Debug.LogError("Message Later Audio source is not assigned in EventManager!");
        }
    }
}
