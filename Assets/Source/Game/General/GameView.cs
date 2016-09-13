using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameView : MonoBehaviour
{
    private Dictionary<long, CellView> cellViews = new Dictionary<long, CellView>();

    public void CreateViews(List<CellData> cells)
    {
        foreach (var item in cells)
        {
            AddView(item);
        }
    }

    private void AddView(CellData data)
    {
        EntityView view = data.CreateView();
        cellViews.Add(data.id, view as CellView);

        CellData mainCell = GameController.Instance.GameModel.MainCell;
        if (mainCell != null && data.id == mainCell.id)
        {
            FollowTarget(view.transform);
        }
    }

    public void UpdateViews(List<CellData> cells)
    {
        HashSet<long> ids = new HashSet<long>();

        foreach (var cell in cells)
        {
            if (cellViews.ContainsKey(cell.id))
            {
                // Update
                CellView view = cellViews[cell.id];
                view.BindData(cell);
            }
            else
            {
                // Add new
                AddView(cell);
            }

            ids.Add(cell.id);
        }

        // Remove not existing cells
        foreach (var entityId in cellViews.Keys)
        {
            if (ids.Contains(entityId) == false)
            {
                EntityView view = cellViews[entityId];
                cellViews.Remove(entityId);
                Destroy(view);
            }
        }
    }

    private void FollowTarget(Transform target)
    {
        CameraFollowTarget follow = Camera.main.GetComponent<CameraFollowTarget>();
        follow.SetTarget(target, false);
    }
}
