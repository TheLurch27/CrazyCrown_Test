using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [Header("•••••••••••Mood-Bar•••••••••••")]
    public Slider slider;

    public Image fillImage;

    public Image Normal;
    public Image Stressed;
    public Image Angry;

    [Header("•••••••••••Life-Icons•••••••••••")]
    public GameObject[] lifeIcons;
    public GameObject[] goneLifeIcons;
    public GameObject GameOverUI;
    private int currentLife = 3;
    private bool isGameOver = false;

    [Header("•••••••••••Slider•••••••••••")]
    public Slider interactionSlider; // Der Slider für die Interaktion
    public Transform characterHead; // Die Position des Charakterkopfes

    public void Start()
    {
        ShowAllIcons();
        HideAllIcons();
        GameOverUI.SetActive(false);
        interactionSlider.gameObject.SetActive(false);
    }

    void Update()
    {

        float sliderNormalizedValue = slider.normalizedValue;

        float sliderValue = slider.value;

        if (sliderValue >= 0.66f)
        {
            ShowImage(Normal);
            HideImage(Stressed);
            HideImage(Angry);
        }
        else if (sliderValue >= 0.34f)
        {
            ShowImage(Stressed);
            HideImage(Normal);
            HideImage(Angry);
        }
        else
        {
            ShowImage(Angry);
            HideImage(Normal);
            HideImage(Stressed);
        }

        UpdateSliderPosition();
    }

    private void ShowImage(Image image)
    {
        image.gameObject.SetActive(true);
    }

    private void HideImage(Image image)
    {
        image.gameObject.SetActive(false);
    }

    public void LoseLife()
    {
        if (isGameOver)
        {
            return;
        }

        currentLife--;

        UpdateLifeDisplay();

        if (currentLife == 0)
        {
            ShowGameOverUI();
        }
    }

    private void UpdateLifeDisplay()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            if (i < currentLife)
            {
                lifeIcons[i].SetActive(true);
            }
            else
            {
                lifeIcons[i].SetActive(false);
            }
        }
    }

    private void ShowAllIcons()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].SetActive(true);
        }
    }

    private void HideAllIcons()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].SetActive(false);
        }
    }

    private void ShowGameOverUI()
    {
        isGameOver = true;
        GameOverUI.SetActive(true);
    }

    public void OnCharacterCaught()
    {
        LoseLife();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name)
            ;
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void UpdateSliderPosition()
    {
        if (interactionSlider.gameObject.activeSelf) // Überprüfe, ob der Slider aktiv ist
        {
            // Aktualisiere die Position des Sliders, um dem Charakterkopf zu folgen
            interactionSlider.transform.position = Camera.main.WorldToScreenPoint(characterHead.position + Vector3.up * 1.5f);
        }
    }

    // Methode, um den Slider anzuzeigen und die Interaktionszeit festzulegen
    public void ShowInteractionSlider(float interactionTime)
    {
        interactionSlider.gameObject.SetActive(true); // Aktiviere den Slider
        interactionSlider.maxValue = interactionTime; // Setze die maximale Interaktionszeit
    }

    // Methode, um den Slider auszublenden
    public void HideInteractionSlider()
    {
        if (interactionSlider != null) // Überprüfe, ob das Slider-Objekt null ist
        {
            interactionSlider.gameObject.SetActive(false); // Deaktiviere den Slider
        }
    }

    // Methode, um die Interaktionszeit im Slider zu aktualisieren
    public void UpdateInteractionSlider(float remainingTime)
    {
        interactionSlider.value = interactionSlider.maxValue - remainingTime; // Aktualisiere den Slider, um herunterzulaufen
    }
}