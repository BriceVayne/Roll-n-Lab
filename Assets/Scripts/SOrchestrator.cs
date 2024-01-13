using System;
using System.Collections.Generic;
using UnityEditor;

namespace Orchestrator
{
    [Serializable]
    internal struct SOrchestrator
    {
        public string Name;
        public ESceneOrder SceneOrder;
        public List<SceneAsset> Scenes;
    }
}