using System;

namespace Layers
{
    [Serializable]
    public struct SLayer
    {
        public string Name;
        public ELayerType Type;
        public Layer Prefab;
        public bool IsEnable;
    }
}