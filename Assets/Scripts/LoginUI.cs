using System;
using System.Threading.Tasks;
using com.example;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginUI : MonoBehaviour
{
    public TMP_InputField EmailField;
    public TMP_InputField PasswordField;
    public TMP_Text StatusText;
    public SupabaseManager SupabaseManager;

    private bool _doSignIn;
    private bool _doSignUp;

    public void SignIn() => _doSignIn = true;
    public void SignUp() => _doSignUp = true;

    private async void Update()
    {
        if (_doSignIn) { _doSignIn = false; await PerformSignIn(); }
        if (_doSignUp) { _doSignUp = false; await PerformSignUp(); }
    }

    private async Task PerformSignIn()
    {
        try
        {
            StatusText.text = "Signing in...";
            var session = await SupabaseManager.Supabase().Auth.SignInWithPassword(
                EmailField.text, 
                PasswordField.text
            );
            StatusText.text = $"Welcome, {session.User?.Email}!";
            await Task.Delay(1000);
            SceneManager.LoadScene("MainMap");
        }
        catch (Exception e)
        {
            var userMessage = e.Message;

            try
            {
                using var json = System.Text.Json.JsonDocument.Parse(e.Message);
                var root = json.RootElement;

                if (root.TryGetProperty("msg", out var msg))
                    userMessage = msg.GetString() ?? userMessage;
                else if (root.TryGetProperty("message", out var message))
                    userMessage = message.GetString() ?? userMessage;
                else if (root.TryGetProperty("error_description", out var errorDescription))
                    userMessage = errorDescription.GetString() ?? userMessage;
                else if (root.TryGetProperty("error", out var error))
                    userMessage = error.GetString() ?? userMessage;
            }
            catch
            {
            }

            StatusText.text = $"Error: {userMessage}";
        }
    }

    private async Task PerformSignUp()
    {
        try
        {
            StatusText.text = "Creating account...";
            await SupabaseManager.Supabase().Auth.SignUp(
                EmailField.text, 
                PasswordField.text
            );
            StatusText.text = "Done! Check your email.";
        }
        catch (Exception e)
        {
            var userMessage = e.Message;

            try
            {
                using var json = System.Text.Json.JsonDocument.Parse(e.Message);
                var root = json.RootElement;

                if (root.TryGetProperty("msg", out var msg))
                    userMessage = msg.GetString() ?? userMessage;
                else if (root.TryGetProperty("message", out var message))
                    userMessage = message.GetString() ?? userMessage;
                else if (root.TryGetProperty("error_description", out var errorDescription))
                    userMessage = errorDescription.GetString() ?? userMessage;
                else if (root.TryGetProperty("error", out var error))
                    userMessage = error.GetString() ?? userMessage;
            }
            catch
            {
            }

            StatusText.text = $"Error: {userMessage}";
        }
    }
}