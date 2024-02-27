using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Methode, die vom Settings Button aufgerufen wird
    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("Settings");
    }

    // Methode, die vom Start Button aufgerufen wird
    public void LoadDisclaimerScene()
    {
        SceneManager.LoadScene("Disclaimer");
    }

    // Methode, die vom Exit Button aufgerufen wird
    public void ExitGame()
    {
        Application.Quit(); // Schlieﬂt die Anwendung
    }
}
