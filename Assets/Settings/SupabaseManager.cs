using System;
using System.IO;
using System.Threading.Tasks;
using Supabase;
using Supabase.Gotrue;
using Supabase.Gotrue.Interfaces;
using UnityEngine;

namespace com.example
{
    public class SupabaseManager : MonoBehaviour
    {
        private Supabase.Client _supabase;

        async void Awake()
        {
            await SetupSupabase();
        }

        private async Task SetupSupabase()
        {
            var configText = Resources.Load<TextAsset>("AppConfig");
            var config = Newtonsoft.Json.JsonConvert.DeserializeObject<AppConfig>(configText.text);

            var options = new SupabaseOptions
            {
                AutoRefreshToken = true
            };

            _supabase = new Supabase.Client(config.supabase_url, config.supabase_anon_key, options);
            
            _supabase.Auth.SetPersistence(new UnitySessionHandler());
            
            await _supabase.InitializeAsync();
            Debug.Log("Supabase ready!");
        }

        public Supabase.Client Supabase() => _supabase;

        [Serializable]
        private class AppConfig
        {
            public string supabase_url;
            public string supabase_anon_key;
        }
    }

    public class UnitySessionHandler : IGotrueSessionPersistence<Session>
    {
        private string SavePath => Path.Combine(Application.persistentDataPath, "supabase_session.json");

        public void SaveSession(Session session)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(session);
            File.WriteAllText(SavePath, json);
        }

        public Session LoadSession()
        {
            if (!File.Exists(SavePath)) return null;
            var json = File.ReadAllText(SavePath);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Session>(json);
        }

        public void DestroySession()
        {
            if (File.Exists(SavePath))
                File.Delete(SavePath);
        }
    }
}