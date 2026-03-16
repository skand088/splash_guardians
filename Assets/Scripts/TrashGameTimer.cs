using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using splash_guardians;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrashGameTimer : MonoBehaviour
{
    public float gameDuration = 30f;
    private float trash_game_timer;
    public Image TimerBarFill;
    public TMP_Text TimerText;
    public ProgressService ProgressService;
    public string LevelKey = "trash";
    public PlayerScript PlayerScoreSource;
    public int DefaultScore;

    private bool _hasEnded;

    void Start()
    {
        Time.timeScale = 1f;
        trash_game_timer = gameDuration;
        if (ProgressService == null) ProgressService = FindAnyObjectByType<ProgressService>();
    }

    void Update()
    {
        if (_hasEnded 
            || TrashGameManager.gameInstance == null 
            || TrashGameManager.gameInstance.gameCurrentState != TrashGameManager.GameState.Playing)
            return;

        trash_game_timer -= Time.deltaTime;
        TimerBarFill.fillAmount = Mathf.Clamp01(trash_game_timer / gameDuration);
        TimerText.text = "Time: " + Mathf.CeilToInt(Mathf.Max(trash_game_timer, 0f));

        if (trash_game_timer <= 0f)
        {
            EndGame();
        }
    }

    async void EndGame()
    {
        _hasEnded = true;
        Debug.Log("Trash game over!");
        if (ProgressService != null) await SaveProgressSafely();
        if (TrashGameManager.gameInstance != null) TrashGameManager.gameInstance.EndGame();
    }

    private async Task SaveProgressSafely()
    {
        try
        {
            var finalScore = PlayerScoreSource != null ? PlayerScoreSource.TrashScore : DefaultScore;
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
