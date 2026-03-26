using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class QuizEndScene : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    public Button restartButton;
    public Button mainMenuButton;

    private void Start()
    {
        // Set score text
        if (scoreText != null)
        {
            scoreText.text = "Score: " + QuizSessionData.FinalScore + "/" + QuizSessionData.TotalQuestions;
        }

        // Restart button
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }

        // Main menu button
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("QuizScene");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMap");
    }
}