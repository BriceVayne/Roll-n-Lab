using Patterns;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Reborn.Menu
{
    internal class SceneLoaderService : Singleton<SceneLoaderService>
    {
        public void LoadSceneSingle(SceneAsset sceneAsset)
        {
            if (sceneAsset != null)
                LoadScene(sceneAsset.name, LoadSceneMode.Single);
        }

        public void LoadSceneAdditive(SceneAsset sceneAsset)
        {
            if (sceneAsset != null)
                LoadScene(sceneAsset.name, LoadSceneMode.Additive);
        }

        public void ExitGame()
        {
            if (Application.isPlaying)
            {
                Debug.Log("Exit Game !");
                Application.Quit();
            }
        }

        private void LoadScene(string sceneName, LoadSceneMode loadSceneMode)
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                SceneManager.LoadScene(sceneName, loadSceneMode);
                Debug.Log($"Scene {sceneName} [LOADED] [{loadSceneMode}]");
            }
        }
    }
}

