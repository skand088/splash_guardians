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

    public Button howToPlayButton;
    public GameObject howToPlayPanel;
    public Button closeHowToPlayButton;

    private void Start()
    {
        if (playButton != null)
        {
            playButton.onClick.AddListener(() => AlgaeGameManager.gameInstance.StartGame());
        }

        if (howToPlayButton != null)
        {
            howToPlayButton.onClick.AddListener(OpenHowToPlay);
        }

        if (closeHowToPlayButton != null)
        {
            closeHowToPlayButton.onClick.AddListener(CloseHowToPlay);
        }


        if (howToPlayPanel != null)
        {
            howToPlayPanel.SetActive(false);
        }
        _ = RefreshScoreTextAsync();
    }

    private void OnEnable()
    {
        if (howToPlayPanel != null)
        {
            howToPlayPanel.SetActive(false);
        }

        _ = RefreshScoreTextAsync();
    }

    public void OpenHowToPlay()
    {
        if (howToPlayPanel != null)
        {
            howToPlayPanel.SetActive(true);
        }
    }

    public void CloseHowToPlay()
    {
        if (howToPlayPanel != null)
        {
            howToPlayPanel.SetActive(false);
        }
    }

    private async Task RefreshScoreTextAsync()
    {
        await LevelScoresDisplayHelper.RefreshAsync(scoreOutputText, ProgressService, EmptyScoresText);
    }
}