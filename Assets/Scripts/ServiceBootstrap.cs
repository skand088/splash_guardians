using UnityEngine;

namespace splash_guardians
{
    public class ServiceBootstrap : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
