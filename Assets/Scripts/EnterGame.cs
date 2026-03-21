using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace splash_guardians
{
    public class EnterGame : MonoBehaviour
    {
        private Dictionary<string, string> PortalNames = new();

        private void Start()
        {
            PortalNames.Add("AlgaeDoor", "AlgaeScene");
            PortalNames.Add("TrashDoor", "TrashScene");
            PortalNames.Add("QuizDoor", "quizgame");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            SceneManager.LoadScene(PortalNames[collision.name]);
        }
    }
}