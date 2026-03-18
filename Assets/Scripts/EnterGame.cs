using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace splash_guardians
{
    public class EnterGame : MonoBehaviour
    {
        private Dictionary<string, int> PortalNames = new();

        private void Start()
        {
            PortalNames.Add("AlgaeDoor", 1);
            PortalNames.Add("TrashDoor", 2);
            PortalNames.Add("QuizDoor", 3);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            SceneManager.LoadScene(PortalNames[collision.name]);
        }
    }
}