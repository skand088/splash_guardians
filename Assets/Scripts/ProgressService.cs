using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.example;
using Supabase.Postgrest;
using UnityEngine;

namespace splash_guardians
{
    public class ProgressService : MonoBehaviour
    {
        public SupabaseManager SupabaseManager;

        private void Awake()
        {
            if (SupabaseManager == null)
            {
                SupabaseManager = FindAnyObjectByType<SupabaseManager>();
            }
        }

        private Supabase.Client GetClientOrThrow()
        {
            if (SupabaseManager == null)
            {
                SupabaseManager = FindAnyObjectByType<SupabaseManager>();
            }

            var client = SupabaseManager != null ? SupabaseManager.Supabase() : null;
            if (client == null)
            {
                throw new InvalidOperationException("Supabase client is not ready yet.");
            }

            return client;
        }

        public bool IsClientReady()
        {
            if (SupabaseManager == null)
            {
                SupabaseManager = FindAnyObjectByType<SupabaseManager>();
            }

            return SupabaseManager != null && SupabaseManager.Supabase() != null;
        }

        public bool HasSignedInUser()
        {
            if (!IsClientReady())
            {
                return false;
            }

            return SupabaseManager.Supabase().Auth.CurrentUser != null;
        }

        public async Task SaveLevelCompletedAsync(string levelKey)
        {
            await SaveLevelResultAsync(levelKey, 0);
        }

        public async Task SaveLevelResultAsync(string levelKey, int score)
        {
            if (string.IsNullOrWhiteSpace(levelKey))
                throw new ArgumentException("levelKey cannot be empty.", nameof(levelKey));

            var client = GetClientOrThrow();
            var user = client.Auth.CurrentUser;

            if (user == null)
            {
                throw new InvalidOperationException("No signed-in user found.");
            }

            var existing = await client
                .From<ProgressRecord>()
                .Filter("user_id", Constants.Operator.Equals, user.Id)
                .Filter("level_key", Constants.Operator.Equals, levelKey)
                .Get();

            if (existing.Models.Count > 0)
            {
                var record = existing.Models[0];

                if (score > record.Score)
                {
                    record.Completed = true;
                    record.Score = score;
                    record.CompletedAt = DateTime.UtcNow;
                    await record.Update<ProgressRecord>();
                }

                return;
            }

            var newRecord = new ProgressRecord
            {
                UserId = user.Id,
                LevelKey = levelKey,
                Completed = true,
                Score = score,
                CompletedAt = DateTime.UtcNow
            };

            await client.From<ProgressRecord>().Insert(newRecord);
        }

        public async Task<HashSet<string>> GetCompletedLevelsAsync()
        {
            var client = GetClientOrThrow();
            var user = client.Auth.CurrentUser;

            if (user == null)
            {
                throw new InvalidOperationException("No signed-in user found.");
            }

            var result = await client
                .From<ProgressRecord>()
                .Filter("user_id", Constants.Operator.Equals, user.Id)
                .Filter("completed", Constants.Operator.Equals, true)
                .Get();

            return result.Models
                .Where(item => !string.IsNullOrWhiteSpace(item.LevelKey))
                .Select(item => item.LevelKey)
                .ToHashSet();
        }

        public async Task<List<ProgressRecord>> GetMyStoredScoresAsync()
        {
            var client = GetClientOrThrow();
            var user = client.Auth.CurrentUser;

            if (user == null)
            {
                throw new InvalidOperationException("No signed-in user found.");
            }

            var result = await client
                .From<ProgressRecord>()
                .Filter("user_id", Constants.Operator.Equals, user.Id)
                .Get();

            return result.Models
                .Where(item => !string.IsNullOrWhiteSpace(item.LevelKey))
                .OrderByDescending(item => item.Score)
                .ThenByDescending(item => item.CompletedAt)
                .ToList();
        }
    }
}