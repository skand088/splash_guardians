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
    public TMP_Text scoreMessageText;

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

        //to display a message to the user based on how they performed
        PlayerScript player = FindFirstObjectByType<PlayerScript>();
        if (player != null && scoreMessageText != null)
        {
            int finalScore = player.AlgaeScore;

            //base on users performance
            if (finalScore < 15)
                scoreMessageText.text = "Uh oh! You only scored " + finalScore + ", you didn't stop the algae!";
            else if (finalScore <= 30)
                scoreMessageText.text = "You scored " + finalScore + "! Try catching more algae next time.";
            else
                scoreMessageText.text = "You did it! You stopped all the algae and scored" + finalScore + "!";
        }
    }

    private async Task RefreshScoreTextAsync()
    {
        await LevelScoresDisplayHelper.RefreshAsync(scoreOutputText, ProgressService, EmptyScoresText);
    }
}
