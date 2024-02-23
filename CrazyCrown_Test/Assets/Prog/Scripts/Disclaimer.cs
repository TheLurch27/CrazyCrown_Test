using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Disclaimer : MonoBehaviour
{
    public Image inputActionImage; // Das UI Image, das nach einer Verz�gerung eingeblendet werden soll
    public float delayTime = 3f; // Die Verz�gerungszeit in Sekunden
    public string sceneToLoad; // Die Name der Szene, die geladen werden soll, wenn der Spieler Enter dr�ckt

    private bool hasDelayPassed = false;

    void Start()
    {
        // Deaktiviere das UI Image zu Beginn
        inputActionImage.enabled = false;

        // Rufe die Funktion ShowDelayedImage nach delayTime Sekunden auf
        Invoke("ShowDelayedImage", delayTime);
    }

    void ShowDelayedImage()
    {
        // Aktiviere das UI Image
        inputActionImage.enabled = true;
        hasDelayPassed = true;
    }

    void Update()
    {
        // �berpr�fe, ob die Verz�gerung abgelaufen ist und der Spieler Enter dr�ckt und das Bild aktiv ist
        if (hasDelayPassed && inputActionImage.enabled && Input.GetKeyDown(KeyCode.Return))
        {
            // Lade die neue Szene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
