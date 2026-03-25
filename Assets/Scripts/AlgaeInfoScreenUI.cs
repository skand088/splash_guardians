using UnityEngine;
using UnityEngine.UI;

public class AlgaeInfoScreenUI : MonoBehaviour
{
    public Button startButton;

    private void Start()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(() => AlgaeGameManager.gameInstance.StartGame());
        }
    }
}