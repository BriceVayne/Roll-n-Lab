using Maze;
using UnityEngine;

namespace Layers
{
    public class Layer : MonoBehaviour
    {
        [SerializeField] private LayerItem m_Prefab;
        private LayerItem[,] m_Items;

        private void OnCellCreate(CellModel cell) 
        {
            LayerItem item = Instantiate(m_Prefab, transform);
            item.InitializeItem(cell);

            m_Items[cell.Position.x, cell.Position.y] = item;
        }

        private void OnCellUpdate(CellModel cell) 
        { 
            m_Items[cell.Position.x, cell.Position.y].UpdateItem(cell); 
        }

        private void Awake()
        {
            m_Items = new LayerItem[GameManager.MazeSize.x, GameManager.MazeSize.y];
        }

        private void Start()
        {
            GameManager.OnCellCreated += OnCellCreate;
            GameManager.OnCellUpdated += OnCellUpdate;
        }
    }
}

