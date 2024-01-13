using Patterns;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Orchestrator
{
    internal sealed class SceneOrchestrator : Singleton<SceneOrchestrator>
    {
        public Action<ESceneOrder> LoadScenes;
        public Action<ESceneOrder> UnloadScenes;

        [SerializeField] private List<SOrchestrator> m_Scenes;
        [SerializeField] private List<ESceneOrder> m_SceneOrder;

        private void Awake()
        {
            LoadScenes = null;
            UnloadScenes = null;

            LoadScenes += LoadSceneByOrder;
            UnloadScenes += UnloadSceneByOrder;
        }

        private void LoadSceneByOrder(ESceneOrder _ESceneOrder)
            => LoadOrUnloadScene(_ESceneOrder);

        private void UnloadSceneByOrder(ESceneOrder _ESceneOrder)
        => LoadOrUnloadScene(_ESceneOrder, false);

        private void LoadOrUnloadScene(ESceneOrder _ESceneOrder, bool _ShouldLoad = true)
        {
            var scenes = m_Scenes.Find(i => i.SceneOrder == _ESceneOrder);
            foreach (var scene in scenes.Scenes)
            {
                if (_ShouldLoad)
                    SceneManager.LoadScene(scene.name);
                else
                    SceneManager.UnloadSceneAsync(scene.name);
            }
        }
    }
}
