using UnityEngine;
using TMPro; // for displaying the timer
using System.Threading.Tasks;
using splash_guardians;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrashGameTimer : MonoBehaviour
{
    public float gameDuration = 30f; //set the game duration
    private float trash_game_timer;
    public Image TimerBarFill;
    public ProgressService ProgressService;
    public string LevelKey = "trash";
    public splash_guardians.PlayerScript PlayerScoreSource;
    public int DefaultScore;

    private bool _hasEnded;

    void Start()
    {
        Time.timeScale = 1f; // Unfreeze time from previous scene
        trash_game_timer = gameDuration;
        if (ProgressService == null)
        {
            ProgressService = FindAnyObjectByType<ProgressService>();
        }
    }

    void Update()
    {
        if (_hasEnded) return;

        //for start/end screen logic, we do not want to update time if we are not playing
        if (TrashGameManager.gameInstance == null ||TrashGameManager.gameInstance.gameCurrentState != TrashGameManager.GameState.Playing)
             return;
        
        trash_game_timer -= Time.deltaTime;//decrement the timer
        //if the timer has completed, end the game
        if (trash_game_timer <= 0)
        {
            TimerBarFill.fillAmount = 0f; // set timer bar to empty
            EndGame();
        } else {
            TimerBarFill.fillAmount = trash_game_timer / gameDuration; // update the timer bar fill amount
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

        TrashGameManager.gameInstance.EndGame();
    }

    private async Task SaveProgressSafely()
    {
        try
        {
            var finalScore = PlayerScoreSource != null ? PlayerScoreSource.TrashScore : DefaultScore;
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
