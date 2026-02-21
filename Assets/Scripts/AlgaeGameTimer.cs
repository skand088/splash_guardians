using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float gameDuration = 30f; //set the game duration
    private float algae_game_timer;

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
            EndGame();
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