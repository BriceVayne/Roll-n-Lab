using Maze;
using UnityEngine;

namespace Layers
{
    public class Layer<TItem> : MonoBehaviour where TItem : LayerItem
    {
        [SerializeField] protected TItem m_Prefab;
        [SerializeField] protected float m_ZOffset;

        protected TItem[,] m_Items;

        protected virtual void OnCellCreate(CellModel cell) 
        {
            TItem item = Instantiate(m_Prefab, transform);
            item.InitializeItem(cell);

            m_Items[cell.Position.x, cell.Position.y] = item;
        }

        protected virtual void OnCellUpdate(CellModel cell) 
        { 
            m_Items[cell.Position.x, cell.Position.y].UpdateItem(cell); 
        }

        protected virtual void AwakeBehaviour()
        {
            m_Items = new TItem[GameManager.MazeSize.x, GameManager.MazeSize.y];
        }

        protected virtual void StartBehaviour()
        {
            GameManager.OnCellCreated += OnCellCreate;
            GameManager.OnCellUpdated += OnCellUpdate;
        }
    }
}

