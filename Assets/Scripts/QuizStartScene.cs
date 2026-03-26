using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizStartScene : MonoBehaviour
{
    public Button playButton;
    public Button mainMenuButton;
    public Button howToPlayButton;       // button to open How To Play
    public Button exitHowToPlayButton;   // button to close How To Play
    public GameObject howToPlayPanel;    // the panel showing instructions

    private void Start()
    {
        // Play button → load quiz
        if (playButton != null)
            playButton.onClick.AddListener(StartQuiz);

        // Main Menu button → return to main map
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);

        // How to Play button → show panel
        if (howToPlayButton != null)
            howToPlayButton.onClick.AddListener(ShowHowToPlay);

        // Exit How to Play button → hide panel
        if (exitHowToPlayButton != null)
            exitHowToPlayButton.onClick.AddListener(HideHowToPlay);

        // Ensure panel starts hidden
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
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

    private void ShowHowToPlay()
    {
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(true);
    }

    private void HideHowToPlay()
    {
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
    }
}