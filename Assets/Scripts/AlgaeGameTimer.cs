using UnityEngine;
using TMPro; // for displaying the timer
using System.Threading.Tasks;
using splash_guardians;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float gameDuration = 30f; //set the game duration
    private float algae_game_timer;
    public TMP_Text TimerText; //to show the timer
    public ProgressService ProgressService;
    public string LevelKey = "algae";
    public splash_guardians.PlayerScript PlayerScoreSource;
    public int DefaultScore;

    private bool _hasEnded;

    void Start()
    {
        Time.timeScale = 1f; // Unfreeze time from previous scene
        algae_game_timer = gameDuration;
        if (ProgressService == null)
        {
            ProgressService = FindAnyObjectByType<ProgressService>();
        }
    }

    void Update()
    {
        if (_hasEnded) return;

        algae_game_timer -= Time.deltaTime;//decrement the timer
        //if the timer has completed, end the game
        if (algae_game_timer <= 0)
        {
            TimerText.text = "Time: 0"; // set timer back to 0
            EndGame();
        }
        else
        {
            TimerText.text = "Time: " + Mathf.CeilToInt(algae_game_timer); //otherwise, display the actual timer value
        }
    }

    async void EndGame()
    {
        _hasEnded = true;
        //display a message in the console
        Debug.Log("Game over!");

        if (ProgressService != null)
        {
            await SaveProgressSafely();
        }

        //keep as 0f for now to end the game, change to 1f for game over screen later
        Time.timeScale = 0f;
    }

    private async Task SaveProgressSafely()
    {
        try
        {
            var finalScore = PlayerScoreSource != null ? PlayerScoreSource.AlgaeScore : DefaultScore;
            await ProgressService.SaveLevelResultAsync(LevelKey, finalScore);
            Debug.Log($"Saved progress for level '{LevelKey}' with score {finalScore}.");
            await Task.Delay(500);
            SceneManager.LoadScene("LoginScene");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to save progress for level '{LevelKey}': {e.Message}");
        }
    }
}
