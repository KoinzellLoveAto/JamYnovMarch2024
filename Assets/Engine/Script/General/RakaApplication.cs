using UnityEngine;

namespace RakaEngine.App
{

    public class RakaApplication : MonoBehaviour
    {
        public static RakaApplication Instance { get; private set; }


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                DestroyImmediate(this.gameObject);
            }
        }

        public void QuitGame()
        {
            Application.Quit();
        }

      
    }
}
