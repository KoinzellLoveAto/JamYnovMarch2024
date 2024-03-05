using RakaEngine.App;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RakaEngine.SceneLoader
{

    public class RootInializer : MonoBehaviour
    {
        [SerializeField]
        private RakaApplication ApplicationPrefab;

        [SerializeField]
        private string NextScene = "MainMenu";

        public void Awake()
        {

            StartCoroutine(LoadRootObject());
        }

        private IEnumerator LoadRootObject()
        {
            Instantiate(ApplicationPrefab);
            yield return new WaitUntil(() => RakaApplication.Instance != null);

            SceneManager.LoadScene(NextScene);
        }

    }
}
