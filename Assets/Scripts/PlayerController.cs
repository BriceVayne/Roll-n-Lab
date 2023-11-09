using Extension;
using Maze;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private float m_RayDistance = 1f;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var view = RaycastThrought2DGrid();

            if (CannotBeSelected(view))
                return;

            if (GameManager.SelectedPath.Count == 0 || GameManager.SelectedPath.Peek().IsNeighboor(view))
                AddCellToPath(view);
        }
        else if (Input.GetMouseButtonUp(1))
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
        if (GameManager.SelectedPath.Contains(cell))
            return;

        cell.Selected();
        GameManager.SelectedPath.Push(cell);
    }

    private void RemoveCellToPath(TwoDimensionCell cell)
    {
        if (!GameManager.SelectedPath.Contains(cell))
            return;

        TwoDimensionCell pathCell;

        do 
        {
            pathCell = GameManager.SelectedPath.Pop();
            pathCell.Deselected();
        } 
        while (pathCell != cell);
    }
}
