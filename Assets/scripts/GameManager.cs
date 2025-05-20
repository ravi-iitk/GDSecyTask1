using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        // Make sure there's only one instance
        if (Instance == null)
        {
            Instance = this;
            // Optional: Uncomment if you want GameManager to persist across scenes
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        ResetGame(); // Reset score and UI when the scene starts
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    public void ResetGame()
    {
        score = 0;
        UpdateScoreUI();
    }
}