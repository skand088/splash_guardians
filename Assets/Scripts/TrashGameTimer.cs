using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using splash_guardians;
using UnityEngine.SceneManagement;

public class TrashGameTimer : MonoBehaviour
{
    public float gameDuration = 30f; // game duration in seconds
    private float trash_game_timer;
    public TMP_Text TimerText; // UI text to show timer
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

        trash_game_timer -= Time.deltaTime; // decrement the timer
        if (trash_game_timer <= 0)
        {
            TimerText.text = "Time: 0";
            EndGame();
        }
        else
        {
            TimerText.text = "Time: " + Mathf.CeilToInt(trash_game_timer);
        }
    }

    async void EndGame()
    {
        _hasEnded = true;
        Debug.Log("Trash game over!");

        if (ProgressService != null)
        {
            await SaveProgressSafely();
        }

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
            SceneManager.LoadScene("AlgaeScene");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to save progress for level '{LevelKey}': {e.Message}");
        }
    }
}