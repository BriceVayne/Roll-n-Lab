using Extension;
using Maze;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private float m_RayDistance = 1f;
    private List<TwoDimensionCell> m_Path;

    private void Start()
    {
        m_Path = new List<TwoDimensionCell>();

    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var view = RaycastThrought2DGrid();

            if (CannotBeSelected(view))
                return;

            if (m_Path.Count == 0 || m_Path.Last().IsNeighboor(view))
                AddCellToPath(view);
        }
        else if (Input.GetMouseButton(1))
        {
            var view = RaycastThrought2DGrid();

            if (CannotBeSelected(view))
                return;

            RemoveCellToPath(view);
        }
    }
    private TwoDimensionCell RaycastThrought2DGrid()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, m_RayDistance);

        if (hit.collider != null)
            return hit.collider.GetComponent<TwoDimensionCell>();
        else
            return null;
    }

    private bool CannotBeSelected(TwoDimensionCell view)
        => view == null || view.Type == ECellType.BORDER || view.Type == ECellType.WALL;

    private void AddCellToPath(TwoDimensionCell cell)
    {
        if (m_Path.Contains(cell))
            return;

        cell.Selected();
        m_Path.Add(cell);
    }

    private void RemoveCellToPath(TwoDimensionCell cell)
    {
        if (!m_Path.Contains(cell))
            return;

        cell.Deselected();
        m_Path.Remove(cell);
    }
}
