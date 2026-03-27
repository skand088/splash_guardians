using UnityEngine;
using UnityEngine.UI;

public class MapMapHowToPlay : MonoBehaviour
{
    public Button howToPlayButton;
    public GameObject howToPlayPanel;
    public Button closeHowToPlayButton;

    private void Start()
    {
        if (howToPlayButton != null)
            howToPlayButton.onClick.AddListener(OpenHowToPlay);

        if (closeHowToPlayButton != null)
            closeHowToPlayButton.onClick.AddListener(CloseHowToPlay);

        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
    }

    public void OpenHowToPlay()
    {
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
    }
}