using UnityEngine;
using UnityEngine.SceneManagement;

public class TrashGameManager : MonoBehaviour
{
    //create a game instance
    public static TrashGameManager gameInstance;

    //define the game state as one of three options
    public enum GameState { Start, Info, Playing, GameOver }
    public GameState gameCurrentState = GameState.Start;


    public GameObject gameStartScreen;
    public GameObject gameOverScreen;
    public GameObject gamePlayScreen; //main game play sceen
    public GameObject gameInfoScreen;

    void Awake()
    {
        if (gameInstance == null)
        {
            gameInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Time.timeScale = 0f;
        gameStartScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        gamePlayScreen.SetActive(false);
        gameInfoScreen.SetActive(false);
    }

    public void ShowInfoScreen()
    {
        gameCurrentState = GameState.Info;
        gameStartScreen.SetActive(false);
        gameInfoScreen.SetActive(true);
    }

    public void StartGame()
    {
        gameCurrentState = GameState.Playing;
        Time.timeScale = 1f;
        gameStartScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        gamePlayScreen.SetActive(true);
        gameInfoScreen.SetActive(false);
    }

    public void EndGame()
    {
        gameCurrentState = GameState.GameOver;
        Time.timeScale = 0f;
        gamePlayScreen.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMap");
    }

}