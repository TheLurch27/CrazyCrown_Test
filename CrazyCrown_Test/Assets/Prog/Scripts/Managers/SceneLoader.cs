using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.instance;

        if (audioManager != null)
        {
            Scene currentScene = SceneManager.GetActiveScene();

            switch (currentScene.name)
            {
                case "MainMenu":
                    audioManager.ChangeMusic(audioManager.backgroundMusicUI);
                    break;
                case "Disclaimer":
                    audioManager.ChangeMusic(audioManager.DisclaimerVoice);
                    break;
                case "Hallway":
                    audioManager.ChangeMusic(audioManager.backgroundGame);
                    break;
                case "ThroneRoom":
                    audioManager.ChangeMusic(audioManager.backgroundGame);
                    break;
                case "BalconyRoom":
                    audioManager.ChangeMusic(audioManager.backgroundGame);
                    break;
                case "SecretRoom":
                    audioManager.ChangeMusic(audioManager.backgroundGame);
                    break;
                case "Settings":
                    audioManager.ChangeMusic(audioManager.backgroundGame);
                    break;
                default:
                    Debug.LogWarning("Scene music not specified for scene: " + currentScene.name);
                    break;
            }
        }
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextScene()
    {
        int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void LoadPreviousScene()
    {
        int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        if (previousSceneIndex < 0)
        {
            previousSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
        }
        SceneManager.LoadScene(previousSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
