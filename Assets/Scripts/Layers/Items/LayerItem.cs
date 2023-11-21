using Maze;
using UnityEngine;

namespace Layers
{
    public abstract class LayerItem : MonoBehaviour
    {
        public abstract void InitializeItem(CellModel cell);

        public abstract void UpdateItem(CellModel cell);
    }
}