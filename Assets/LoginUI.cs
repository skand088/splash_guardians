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
            // uncomment for next game scene:
            // SceneManager.LoadScene("GameScene"); - prolly main menu or mini game 1?
        }
        catch (Exception e)
        {
            StatusText.text = $"Error: {e.Message}";
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
            StatusText.text = $"Error: {e.Message}";
        }
    }
}