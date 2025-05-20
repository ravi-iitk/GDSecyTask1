using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject levelCompletePanel;

    [Header("Audio")]
    public AudioClip gameOverSound;
    public AudioClip levelCompleteSound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        PlaySound(gameOverSound);
    }

    public void ShowLevelComplete()
    {
        levelCompletePanel.SetActive(true);
        PlaySound(levelCompleteSound);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}