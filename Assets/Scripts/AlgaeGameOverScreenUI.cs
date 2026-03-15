using UnityEngine;
using UnityEngine.UI;

public class AlgaeGameOverScreenUI : MonoBehaviour
{
    public Button restartButton;

    void Start()
    {
        restartButton.onClick.AddListener(() => AlgaeGameManager.gameInstance.RestartGame());
    }
}
