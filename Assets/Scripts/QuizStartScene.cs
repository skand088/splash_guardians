using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizStartScene : MonoBehaviour
{
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject titleText;

    private void Start()
    {
        if (infoPanel != null)
            infoPanel.SetActive(false);

        if (titleText != null)
            titleText.SetActive(true);
    }

    public void ShowInfo()
    {
        if (infoPanel != null)
            infoPanel.SetActive(true);

        if (titleText != null)
            titleText.SetActive(false);
    }

    public void HideInfo()
    {
        if (infoPanel != null)
            infoPanel.SetActive(false);

        if (titleText != null)
            titleText.SetActive(true);
    }

    public void StartQuiz()
    {
        SceneManager.LoadScene("QuizScene");
    }
}