using UnityEngine;
using TMPro; // for displaying the timer

public class GameTimer : MonoBehaviour
{
    public float gameDuration = 30f; //set the game duration
    private float algae_game_timer;
    public TMP_Text TimerText; //to show the timer

    void Start()
    {
        algae_game_timer = gameDuration;
    }

    void Update()
    {
        algae_game_timer -= Time.deltaTime;//decrement the timer
        //if the timer has completed, end the game
        if (algae_game_timer <= 0)
        {
            TimerText.text = "Time: 0"; // set timer back to 0
            EndGame();
        } else {
            TimerText.text = "Time: " + Mathf.CeilToInt(algae_game_timer); //otherwise, display the actual timer value
        }
    }

    void EndGame()
    {
        //display a message in the console
        Debug.Log("Game over!");
        //keep as 0f for now to end the game, change to 1f for game over screen later
        Time.timeScale = 0f;
    }
}