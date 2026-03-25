using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class QuizEndScene : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + QuizSessionData.FinalScore + "/" + QuizSessionData.TotalQuestions;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("QuizStartScene");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMap");
    }
}