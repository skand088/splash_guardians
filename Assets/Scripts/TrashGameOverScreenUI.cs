using UnityEngine;
using UnityEngine.UI;
using TMPro;
using splash_guardians;
using System.Threading.Tasks;

public class TrashGameOverScreenUI : MonoBehaviour
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
            restartButton.onClick.AddListener(() => TrashGameManager.gameInstance.RestartGame());
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
            int finalScore = player.TrashScore;

            //base on users performance
            if (finalScore < 10)
                scoreMessageText.text = "Uh oh, the ocean is still polluted! You only scored " + finalScore + ", you didn't catch enough trash!";
            else if (finalScore <= 20)
                scoreMessageText.text = "You scored " + finalScore + "! Try catching more trash next time.";
            else
                scoreMessageText.text = "You did it! You caught all the trash and scored " + finalScore + "!";
        }
    }

    private async Task RefreshScoreTextAsync()
    {
        await LevelScoresDisplayHelper.RefreshAsync(scoreOutputText, ProgressService, EmptyScoresText);
    }
}
