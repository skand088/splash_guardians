using UnityEngine;
using UnityEngine.UI;

public class TrashGameOverScreenUI : MonoBehaviour
{
    public Button restartButton;

    void Start()
    {
        restartButton.onClick.AddListener(() => TrashGameManager.gameInstance.RestartGame());
    }
}
