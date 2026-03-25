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

    public GameObject QuizPanel;
    public GameObject TimerUI;

    public Image timerFill;
    public float timePerQuestion = 30f;

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

        if (QuizPanel != null)
            QuizPanel.SetActive(true);

        if (TimerUI != null)
            TimerUI.SetActive(true);

        if (ProgressService == null)
        {
            ProgressService = FindAnyObjectByType<ProgressService>();
        }

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

        if (TimerUI != null)
            TimerUI.SetActive(false);

        if (QuizPanel != null)
            QuizPanel.SetActive(false);

        QuizSessionData.FinalScore = score;
        QuizSessionData.TotalQuestions = TotalQuestions;

        if (ProgressService != null)
        {
            await SaveProgressSafely();
        }
        else
        {
            Debug.LogWarning("ProgressService not found. Quiz score was not saved.");
        }

        SceneManager.LoadScene("QuizEndScene");
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

            if (TimerUI != null)
                TimerUI.SetActive(true);

            currentTime = timePerQuestion;
            timerRunning = true;

            if (timerFill != null)
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

            if (timerFill != null)
                timerFill.fillAmount = currentTime / timePerQuestion;

            if (currentTime <= 0f)
            {
                currentTime = 0f;

                if (timerFill != null)
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