using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizStartScene : MonoBehaviour
{
    public Button playButton;
    public Button mainMenuButton;

    private void Start()
    {
        // Play button → load quiz
        if (playButton != null)
        {
            playButton.onClick.AddListener(StartQuiz);
        }

        // Main Menu button
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        }
    }

    public void StartQuiz()
    {
        Debug.Log("Loading Quiz Scene...");
        SceneManager.LoadScene("QuizScene");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMap");
    }
}