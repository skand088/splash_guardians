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
        //for start/end screen logic, we do not want to update time if we are not playing
        if (AlgaeGameManager.gameInstance == null ||AlgaeGameManager.gameInstance.gameCurrentState != AlgaeGameManager.GameState.Playing)
             return;
        
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
        //call the endgame screen from the game manager
        Debug.Log("Game over!");
        AlgaeGameManager.gameInstance.EndGame();
    }
}