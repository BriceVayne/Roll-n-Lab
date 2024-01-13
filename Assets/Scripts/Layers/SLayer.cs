using System;

namespace Layers
{
    [Serializable]
    internal struct SLayer
    {
        public string Name;
        public ELayerType Type;
        public Layer Prefab;
        public bool IsEnable;
    }
}