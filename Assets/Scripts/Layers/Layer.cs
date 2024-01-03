using Maze;
using UnityEngine;

namespace Layers
{
    public class Layer : MonoBehaviour
    {
        [SerializeField] private LayerItem m_Prefab;
        private LayerItem[,] m_Items;

        private void OnCellCreate(CellModel _Cell) 
        {
            LayerItem item = Instantiate(m_Prefab, transform);
            item.InitializeItem(_Cell);

            m_Items[_Cell.Position.x, _Cell.Position.y] = item;
        }

        private void OnCellUpdate(CellModel _Cell) 
        { 
            m_Items[_Cell.Position.x, _Cell.Position.y].UpdateItem(_Cell); 
        }

        private void Awake()
        {
            m_Items = new LayerItem[GridManager.Instance.MazeSize.x, GridManager.Instance.MazeSize.y];
        }

        private void Start()
        {
            GridManager.Instance.OnCellCreated += OnCellCreate;
            GridManager.Instance.OnCellUpdated += OnCellUpdate;
        }
    }
}

