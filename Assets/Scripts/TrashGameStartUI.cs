using UnityEngine;
using UnityEngine.UI;

public class TrashGameStartUI : MonoBehaviour
{
    public Button playButton;

    void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            Debug.Log("Trash play button clicked");
            TrashGameManager.gameInstance.StartGame();
        });
    }
}
