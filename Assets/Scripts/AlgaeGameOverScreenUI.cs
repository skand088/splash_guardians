using UnityEngine;
using UnityEngine.UI;
using TMPro;
using splash_guardians;
using System.Threading.Tasks;

public class AlgaeGameOverScreenUI : MonoBehaviour
{
    public Button restartButton;
    public TMP_Text scoreOutputText;
    public ProgressService ProgressService;
    public string EmptyScoresText = "No scores yet.";

    private void Start()
    {
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(() => AlgaeGameManager.gameInstance.RestartGame());
        }

        _ = RefreshScoreTextAsync();
    }

    private void OnEnable()
    {
        _ = RefreshScoreTextAsync();
    }

    private async Task RefreshScoreTextAsync()
    {
        await LevelScoresDisplayHelper.RefreshAsync(scoreOutputText, ProgressService, EmptyScoresText);
    }
}
