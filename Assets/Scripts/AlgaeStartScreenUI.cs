using UnityEngine;
using UnityEngine.UI;
using TMPro;
using splash_guardians;
using System.Threading.Tasks;

public class AlgaeStartScreenUI : MonoBehaviour
{
    public Button playButton;
    public TMP_Text scoreOutputText;
    public ProgressService ProgressService;
    public string EmptyScoresText = "No scores yet.";

    private void Start()
    {
        if (playButton != null)
        {
            playButton.onClick.AddListener(() => AlgaeGameManager.gameInstance.StartGame());
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
