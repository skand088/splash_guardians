using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;

    void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused) Resume();
            else if (CanPause()) Pause();
        }
    }

    private bool CanPause()
    {
        string scene = SceneManager.GetActiveScene().name;

        if (scene == "AlgaeScene")
            return AlgaeGameManager.gameInstance != null &&
                   AlgaeGameManager.gameInstance.gameCurrentState == AlgaeGameManager.GameState.Playing;

        if (scene == "TrashScene")
            return TrashGameManager.gameInstance != null &&
                   TrashGameManager.gameInstance.gameCurrentState == TrashGameManager.GameState.Playing;

        //pause options should always be allowed
        return true;
    }

    void Pause()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMap");
    }
}