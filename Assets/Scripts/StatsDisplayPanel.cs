using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace splash_guardians
{
    /// <summary>
    /// Displays player stats in a panel.
    /// Shows current stats for each game using LevelScoresDisplayHelper.
    /// Use with StatsButtonToggle to open/close the panel.
    /// </summary>
    public class StatsDisplayPanel : MonoBehaviour
    {
        [SerializeField] private ProgressService progressService;
        [SerializeField] private TMP_Text statsText;
        [SerializeField] private Image panelBackground;
        
        [SerializeField] private string emptyStatsText = "No stats yet";
        [SerializeField] private float refreshInterval = 5f; // Refresh every 5 seconds
        [SerializeField] private bool autoRefresh = false; // Set to false when using button toggle

        private float timeSinceLastRefresh = 0f;
        private Canvas parentCanvas;

        private void Awake()
        {
            // Ensure we have the necessary components
            if (statsText == null)
            {
                Debug.LogError("StatsDisplayPanel: statsText is not assigned!");
            }

            if (progressService == null)
            {
                progressService = FindAnyObjectByType<ProgressService>();
            }
        }

        private void Start()
        {
            // Find parent canvas for proper layering
            parentCanvas = GetComponentInParent<Canvas>();
            
            // Refresh stats on start
            _ = RefreshStatsAsync();
        }

        private void Update()
        {
            if (!autoRefresh) return;

            timeSinceLastRefresh += Time.deltaTime;
            if (timeSinceLastRefresh >= refreshInterval)
            {
                _ = RefreshStatsAsync();
                timeSinceLastRefresh = 0f;
            }
        }

        /// <summary>
        /// Manually refresh the stats display.
        /// </summary>
        public async Task RefreshStatsAsync()
        {
            await LevelScoresDisplayHelper.RefreshAsync(statsText, progressService, emptyStatsText);
        }

        /// <summary>
        /// Enable or disable auto-refresh.
        /// </summary>
        public void SetAutoRefresh(bool enabled)
        {
            autoRefresh = enabled;
        }

        /// <summary>
        /// Set the refresh interval in seconds.
        /// </summary>
        public void SetRefreshInterval(float interval)
        {
            refreshInterval = Mathf.Max(0.5f, interval); // Minimum 0.5 seconds
        }

        /// <summary>
        /// Show or hide the stats panel.
        /// </summary>
        public void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }
}
