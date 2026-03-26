using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using splash_guardians;
using PlayerScript = splash_guardians.PlayerScript;

public class TrashGameTimer : MonoBehaviour
{
    public float gameDuration = 30f;
    private float trash_game_timer;
    public Image TimerBarFill;
    public ProgressService ProgressService;
    public string LevelKey = "trash";
    public PlayerScript PlayerScoreSource;
    public int DefaultScore;

    private bool _hasEnded;

    void Start()
    {
        Time.timeScale = 1f;
        trash_game_timer = gameDuration;
        if (ProgressService == null)
        {
            ProgressService = FindAnyObjectByType<ProgressService>();
        }
        if (PlayerScoreSource == null)
        {
            PlayerScoreSource = FindAnyObjectByType<PlayerScript>();
        }
    }

    void Update()
    {
        if (_hasEnded) return;

        if (TrashGameManager.gameInstance == null || TrashGameManager.gameInstance.gameCurrentState != TrashGameManager.GameState.Playing)
            return;

        trash_game_timer -= Time.deltaTime;

        if (trash_game_timer <= 0)
        {
            TimerBarFill.fillAmount = 0f;
            EndGame();
        }
        else
        {
            TimerBarFill.fillAmount = trash_game_timer / gameDuration;
        }
    }

    async void EndGame()
    {
        _hasEnded = true;
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
            // smth like this add for each timer
            // SceneManager.LoadScene("MainMap");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to save progress for level '{LevelKey}': {e.Message}");
        }
    }
}