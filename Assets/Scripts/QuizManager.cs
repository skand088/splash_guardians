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
    public GameObject[] options; //button
    public int currentQuestion;

    public GameObject QuizPanel;
    public GameObject GameOverPanel;

    public Text QuestionText;
    public Text ScoreText;

    int TotalQuestions;
    public int score;

    public ProgressService ProgressService;
    public string LevelKey = "quizgame";
    private bool _hasEnded;

    private void Start()
    {
        TotalQuestions = QnA.Count;
        GameOverPanel.SetActive(false);
        _hasEnded = false;
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

    public void GameOver()
    {
        if (_hasEnded) return;
        _hasEnded = true;
        
        QuizPanel.SetActive(false);
        GameOverPanel.SetActive(true);
        ScoreText.text = "Score: " + score + "/" + TotalQuestions;
        
        if (ProgressService != null)
        {
            _ = SaveProgressSafely();
        }
    }

    private async Task SaveProgressSafely()
    {
        try
        {
            await ProgressService.SaveLevelResultAsync(LevelKey, score);
            Debug.Log($"Saved quiz progress with score {score}/{TotalQuestions}.");
            await Task.Delay(500);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to save quiz progress: {e.Message}");
        }
    }

    public void correct()
    {
        score += 1;
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    public void wrong()
    {
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if (QnA.Count > 0)
        {
             currentQuestion = Random.Range(0, QnA.Count);
            QuestionText.text = QnA[currentQuestion].Question;
            SetAnswers();
        }
        else
        {
            Debug.Log("No Questions Left");
            GameOver();
        }
    }

}
