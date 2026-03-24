using UnityEngine;
using UnityEngine.UI;

namespace splash_guardians
{
    /// <summary>
    /// Button controller that toggles the stats panel visibility.
    /// Place this on a button in the top-right corner to open/close the stats panel.
    /// </summary>
    public class StatsButtonToggle : MonoBehaviour
    {
        [SerializeField] private Button statsButton;
        [SerializeField] private GameObject statsPanelRoot;
        [SerializeField] private StatsDisplayPanel statsDisplayPanel;
        
        private bool isPanelOpen = false;

        private void Awake()
        {
            // Auto-find components if not assigned
            if (statsButton == null)
            {
                statsButton = GetComponent<Button>();
            }

            if (statsPanelRoot == null)
            {
                Debug.LogError("StatsButtonToggle: statsPanelRoot is not assigned!");
            }

            if (statsDisplayPanel == null && statsPanelRoot != null)
            {
                statsDisplayPanel = statsPanelRoot.GetComponent<StatsDisplayPanel>();
            }
        }

        private void Start()
        {
            // Set up button listener
            if (statsButton != null)
            {
                statsButton.onClick.AddListener(TogglePanel);
            }

            // Ensure panel starts closed
            if (statsPanelRoot != null)
            {
                statsPanelRoot.SetActive(false);
                isPanelOpen = false;
            }
        }

        /// <summary>
        /// Toggle the stats panel visibility.
        /// </summary>
        public void TogglePanel()
        {
            isPanelOpen = !isPanelOpen;
            
            if (statsPanelRoot != null)
            {
                statsPanelRoot.SetActive(isPanelOpen);
            }

            // Refresh stats when opening the panel
            if (isPanelOpen && statsDisplayPanel != null)
            {
                _ = statsDisplayPanel.RefreshStatsAsync();
            }
        }

        /// <summary>
        /// Close the stats panel.
        /// </summary>
        public void ClosePanel()
        {
            if (isPanelOpen)
            {
                TogglePanel();
            }
        }

        /// <summary>
        /// Open the stats panel.
        /// </summary>
        public void OpenPanel()
        {
            if (!isPanelOpen)
            {
                TogglePanel();
            }
        }

        private void OnDestroy()
        {
            if (statsButton != null)
            {
                statsButton.onClick.RemoveListener(TogglePanel);
            }
        }
    }
}
