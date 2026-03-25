using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using splash_guardians;
using UnityEngine.EventSystems;

public class QuizManager : MonoBehaviour
{
    [Header("Quiz Data")]
    public List<QuestionAndAnswers> QnA = new List<QuestionAndAnswers>();
    public int currentQuestion;

    [Header("Answer Buttons")]
    public GameObject[] options;
    public Text[] optionTexts;

    [Header("UI")]
    public GameObject QuizPanel;
    public GameObject TimerUI;
    public Image timerFill;
    public Text QuestionText;
    public Text ScoreText;

    [Header("Timer")]
    public float timePerQuestion = 30f;
    private float currentTime;
    private bool timerRunning = false;

    [Header("Progress")]
    public ProgressService ProgressService;
    public string LevelKey = "quiz";

    private bool _hasEnded = false;
    private int TotalQuestions;
    public int score;
    private int questionsAsked = 0;

    private void Start()
    {
        if (QnA == null || QnA.Count == 0)
        {
            Debug.LogError("QuizManager: QnA list is empty. Add questions in the Inspector.");
            return;
        }

        if (options == null || options.Length == 0)
        {
            Debug.LogError("QuizManager: options array is empty.");
            return;
        }

        if (optionTexts == null || optionTexts.Length != options.Length)
        {
            Debug.LogError("QuizManager: optionTexts must be assigned and match options length.");
            return;
        }

        if (QuestionText == null)
        {
            Debug.LogError("QuizManager: QuestionText is not assigned.");
            return;
        }

        TotalQuestions = Mathf.Min(5, QnA.Count);
        score = 0;
        questionsAsked = 0;
        _hasEnded = false;

        if (QuizPanel != null)
            QuizPanel.SetActive(true);

        if (TimerUI != null)
            TimerUI.SetActive(true);

        if (ProgressService == null)
        {
            ProgressService = FindAnyObjectByType<ProgressService>();
        }

        GenerateQuestion();
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        if (_hasEnded) return;

        timerRunning = false;
        score += 1;
        questionsAsked += 1;

        RemoveCurrentQuestionSafely();
        GenerateQuestion();
    }

    public void wrong()
    {
        if (_hasEnded) return;

        timerRunning = false;
        questionsAsked += 1;

        RemoveCurrentQuestionSafely();
        GenerateQuestion();
    }

    private void RemoveCurrentQuestionSafely()
    {
        if (QnA != null && currentQuestion >= 0 && currentQuestion < QnA.Count)
        {
            QnA.RemoveAt(currentQuestion);
        }
        else
        {
            Debug.LogWarning("QuizManager: Tried to remove invalid question index.");
        }
    }

    private void SetAnswers()
    {
        if (QnA == null || QnA.Count == 0)
        {
            Debug.LogError("QuizManager: No questions available in SetAnswers.");
            return;
        }

        QuestionAndAnswers selectedQuestion = QnA[currentQuestion];

        if (selectedQuestion.Answers == null)
        {
            Debug.LogError("QuizManager: Selected question has null Answers array.");
            return;
        }

        for (int i = 0; i < options.Length; i++)
        {
            AnswerScript answerScript = options[i] != null ? options[i].GetComponent<AnswerScript>() : null;

            if (answerScript == null)
            {
                Debug.LogError($"QuizManager: AnswerScript missing on option index {i}.");
                continue;
            }

            answerScript.isCorrect = false;

            if (optionTexts[i] == null)
            {
                Debug.LogError($"QuizManager: optionTexts[{i}] is not assigned.");
                continue;
            }

            if (i < selectedQuestion.Answers.Length)
            {
                optionTexts[i].text = selectedQuestion.Answers[i];
            }
            else
            {
                optionTexts[i].text = "";
                Debug.LogError($"QuizManager: Missing answer data at index {i} for question: {selectedQuestion.Question}");
            }

            if (selectedQuestion.CorrectAnswer == i + 1)
            {
                answerScript.isCorrect = true;
            }
        }

        Canvas.ForceUpdateCanvases();
    }

    private void GenerateQuestion()
    {
        if (_hasEnded) return;

        if (questionsAsked < TotalQuestions && QnA != null && QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);

            QuestionAndAnswers selectedQuestion = QnA[currentQuestion];

            if (selectedQuestion == null)
            {
                Debug.LogError("QuizManager: Selected question is null.");
                GameOver();
                return;
            }

            QuestionText.text = selectedQuestion.Question;

            if (EventSystem.current != null)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }

            SetAnswers();
            Canvas.ForceUpdateCanvases();

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
        if (!timerRunning || _hasEnded) return;

        currentTime -= Time.deltaTime;

        if (timerFill != null)
        {
            timerFill.fillAmount = Mathf.Clamp01(currentTime / timePerQuestion);
        }

        if (currentTime <= 0f)
        {
            currentTime = 0f;

            if (timerFill != null)
                timerFill.fillAmount = 0f;

            timerRunning = false;
            wrong();
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