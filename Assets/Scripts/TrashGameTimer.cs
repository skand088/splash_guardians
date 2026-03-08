using UnityEngine;
using TMPro;

public class TrashGameTimer : MonoBehaviour
{
    public float gameDuration = 30f; // game duration in seconds
    private float trash_game_timer;
    public TMP_Text TimerText; // UI text to show timer

    void Start()
    {
        trash_game_timer = gameDuration;
    }

    void Update()
    {
        trash_game_timer -= Time.deltaTime; // decrement the timer
        if (trash_game_timer <= 0)
        {
            TimerText.text = "Time: 0";
            EndGame();
        }
        else
        {
            TimerText.text = "Time: " + Mathf.CeilToInt(trash_game_timer);
        }
    }
    void EndGame()
    {
        Debug.Log("Trash game over!");
        Time.timeScale = 0f;
    }
}
