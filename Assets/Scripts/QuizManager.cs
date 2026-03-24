using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using splash_guardians;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public GameObject StartPanel;
    public GameObject InfoPanel;
    public GameObject QuizPanel;
    public GameObject GameOverPanel;
    public GameObject TimerUI;

    public Image timerFill;
    public float timePerQuestion = 45f;

    private float currentTime;
    private bool timerRunning = false;

    public Text QuestionText;
    public Text ScoreText;

    public ProgressService ProgressService;
    public string LevelKey = "quiz";

    private bool _hasEnded = false;

    int TotalQuestions;
    public int score;
    int questionsAsked = 0;

    private void Start()
    {
        TotalQuestions = Mathf.Min(5, QnA.Count);

        StartPanel.SetActive(true);
        InfoPanel.SetActive(false);
        QuizPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        TimerUI.SetActive(false);

        if (ProgressService == null)
        {
            ProgressService = FindAnyObjectByType<ProgressService>();
        }
    }

    public void ShowInfo()
    {
        InfoPanel.SetActive(true);
    }

    public void HideInfo()
    {
        InfoPanel.SetActive(false);
    }

    public void StartGame()
    {
        StartPanel.SetActive(false);
        InfoPanel.SetActive(false);
        QuizPanel.SetActive(true);
        TimerUI.SetActive(true);
        generateQuestion();
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public async void GameOver()
    {
        if (_hasEnded) return;
        _hasEnded = true;

        timerRunning = false;
        TimerUI.SetActive(false);

        QuizPanel.SetActive(false);
        GameOverPanel.SetActive(true);
        ScoreText.text = "Score: " + score + "/" + TotalQuestions;

        if (ProgressService != null)
        {
            await SaveProgressSafely();
        }
        else
        {
            Debug.LogWarning("ProgressService not found. Quiz score was not saved.");
        }
    }

    public void correct()
    {
        timerRunning = false;
        score += 1;
        questionsAsked += 1;
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    public void wrong()
    {
        timerRunning = false;
        questionsAsked += 1;
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if (questionsAsked < TotalQuestions && QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);
            QuestionText.text = QnA[currentQuestion].Question;
            SetAnswers();

            TimerUI.SetActive(true);
            currentTime = timePerQuestion;
            timerRunning = true;
            timerFill.fillAmount = 1f;
        }
        else
        {
            Debug.Log("No Questions Left");
            GameOver();
        }
    }

    private void Update()
    {
        if (timerRunning)
        {
            currentTime -= Time.deltaTime;
            timerFill.fillAmount = currentTime / timePerQuestion;

            if (currentTime <= 0f)
            {
                currentTime = 0f;
                timerFill.fillAmount = 0f;
                timerRunning = false;
                wrong();
            }
        }
    }

    private async Task SaveProgressSafely()
    {
        try
        {
            await ProgressService.SaveLevelResultAsync(LevelKey, score);
            Debug.Log($"Saved progress for level '{LevelKey}' with score {score}.");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to save progress for level '{LevelKey}': {e.Message}");
        }
    }
}