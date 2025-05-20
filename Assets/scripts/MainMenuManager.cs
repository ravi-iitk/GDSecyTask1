using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Optional Sound")]
    public AudioSource buttonClickSound;

    [Header("Panels")]
    public GameObject creditsPanel;

    // Called when "Play" button is clicked
    public void PlayGame()
    {
        PlaySound();
        Invoke(nameof(LoadLevel1), 0.2f);
    }

    void LoadLevel1()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single); // Ensure Level1 is in Build Settings
    }

    // Called when "Quit" button is clicked
    public void QuitGame()
    {
        PlaySound();
        Invoke(nameof(QuitApplication), 0.2f);
    }

    void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Quit Game"); // Only visible in editor
    }

    // Called when "Credits" button is clicked
    public void ShowCredits()
    {
        PlaySound();
        creditsPanel.SetActive(true);
    }

    // Called by a "Close" button inside the Credits panel
    public void HideCredits()
    {
        PlaySound();
        creditsPanel.SetActive(false);
    }

    void PlaySound()
    {
        if (buttonClickSound != null)
            buttonClickSound.Play();
    }
}