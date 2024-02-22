using UnityEngine;
using UnityEngine.InputSystem;

public class DoorController : MonoBehaviour
{
    public string sceneToLoad;

    private bool playerInRange = false;

    /// <summary>
    /// When the player enters the trigger zone, set the playerInRange flag to true.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered the door trigger zone.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player exited the door trigger zone.");
        }
    }

    private void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        Debug.Log("Loading scene: " + sceneToLoad);


        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }
}
