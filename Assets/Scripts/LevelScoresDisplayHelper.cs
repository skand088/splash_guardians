using System;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace splash_guardians
{
    public static class LevelScoresDisplayHelper
    {
        public static async Task RefreshAsync(TMP_Text outputText, ProgressService progressService, string emptyScoresText)
        {
            if (outputText == null)
            {
                return;
            }

            if (progressService == null)
            {
                progressService = UnityEngine.Object.FindAnyObjectByType<ProgressService>();
            }

            if (progressService == null)
            {
                outputText.text = emptyScoresText;
                return;
            }

            try
            {
                var records = await progressService.GetMyStoredScoresAsync();

                if (records == null || records.Count == 0)
                {
                    outputText.text = emptyScoresText;
                    return;
                }

                var levelLines = records
                    .Where(record => !string.IsNullOrWhiteSpace(record.LevelKey))
                    .GroupBy(record => record.LevelKey, StringComparer.OrdinalIgnoreCase)
                    .Select(group => new
                    {
                        Name = FormatLevelName(group.Key),
                        MaxScore = group.Max(item => item.Score)
                    })
                    .OrderBy(item => item.Name)
                    .Select(item => $"Max {item.Name}: {item.MaxScore}")
                    .ToArray();

                outputText.text = levelLines.Length > 0
                    ? string.Join("\n", levelLines)
                    : emptyScoresText;
            }
            catch
            {
                outputText.text = emptyScoresText;
            }
        }

        private static string FormatLevelName(string levelKey)
        {
            if (string.IsNullOrWhiteSpace(levelKey)) return "Unknown";
            if (string.Equals(levelKey, "quizgame", StringComparison.OrdinalIgnoreCase)) return "Quiz";
            return char.ToUpperInvariant(levelKey[0]) + levelKey[1..];
        }
    }
}