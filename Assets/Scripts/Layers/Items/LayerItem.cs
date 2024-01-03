using Maze;
using UnityEngine;

namespace Layers
{
    public abstract class LayerItem : MonoBehaviour
    {
        public abstract void InitializeItem(CellModel _Cell);

        public abstract void UpdateItem(CellModel _Cell);
    }
}