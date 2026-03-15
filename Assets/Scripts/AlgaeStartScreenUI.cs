using UnityEngine;
using UnityEngine.UI;

public class AlgaeStartScreenUI : MonoBehaviour
{
    public Button playButton;

    void Start()
    {
        playButton.onClick.AddListener(() => AlgaeGameManager.gameInstance.StartGame());
    }
}
