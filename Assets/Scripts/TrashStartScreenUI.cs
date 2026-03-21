using UnityEngine;
using UnityEngine.UI;

public class TrashStartScreenUI : MonoBehaviour
{
    public Button playButton;

    void Start()
    {
        playButton.onClick.AddListener(() => TrashGameManager.gameInstance.StartGame());
    }
}
