using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizStartScreen : MonoBehaviour
{
    public GameObject infoPanel;

    void Start()
    {
        if (infoPanel != null)
            infoPanel.SetActive(false);
    }

    public void ShowInfo()
    {
        if (infoPanel != null)
            infoPanel.SetActive(true);
    }

    public void HideInfo()
    {
        if (infoPanel != null)
            infoPanel.SetActive(false);
    }

    public void StartQuiz()
    {
        SceneManager.LoadScene("QuizScene");
    }
}