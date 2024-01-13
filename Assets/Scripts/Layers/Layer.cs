using Maze;
using Service;
using UnityEngine;

namespace Layers
{
    internal sealed class Layer : MonoBehaviour
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
            m_Items = new LayerItem[GridService.Instance.MazeSize.x, GridService.Instance.MazeSize.y];
        }

        private void Start()
        {
            GridService.Instance.OnCellCreated += OnCellCreate;
            GridService.Instance.OnCellUpdated += OnCellUpdate;
        }
    }
}

