using UnityEngine;
using UnityEngine.SceneManagement;

namespace splash_guardians
{
    public class EnterAlgaeGame : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            SceneManager.LoadScene(2);
        }
    }
}