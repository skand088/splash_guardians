using UnityEngine;
using TMPro; // for displaying the timer
using UnityEngine.UI;

public class AlgaeGameTimer : MonoBehaviour
{
    public float gameDuration = 30f; //set the game duration
    private float algae_game_timer;
    public Image TimerBarFill;

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
            TimerBarFill.fillAmount = 0f; // set timer bar to empty
            EndGame();
        } else {
            TimerBarFill.fillAmount = algae_game_timer / gameDuration; // update the timer bar fill amount
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