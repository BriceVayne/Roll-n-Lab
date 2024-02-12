using Patterns;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Reborn.Menu
{
    internal class SceneLoader : Singleton<SceneLoader>
    {
        public static void LoadSceneSingle(SceneAsset sceneAsset)
        {
            if (sceneAsset != null)
                LoadScene(sceneAsset.name, LoadSceneMode.Single);
        }

        public static void LoadSceneAdditive(SceneAsset sceneAsset)
        {
            if (sceneAsset != null)
                LoadScene(sceneAsset.name, LoadSceneMode.Additive);
        }

        public static void ExitGame()
        {
            if (Application.isPlaying)
            {
                Debug.Log("Exit Game !");
                Application.Quit();
            }
        }

        private static void LoadScene(string sceneName, LoadSceneMode loadSceneMode)
        {
            if (!string.IsNullOrEmpty(sceneName))
                SceneManager.LoadScene(sceneName, loadSceneMode);
        }
    }
}

