using UnityEngine;
using UnityEngine.UI;
public class TrashInfoScreenUI : MonoBehaviour
{
    public Button startButton;
    private void Start()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(() => TrashGameManager.gameInstance.StartGame());
        }
    }
}