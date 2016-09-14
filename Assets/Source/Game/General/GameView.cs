using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameView : MonoBehaviour
{
    private Dictionary<long, CellView> cellViews = new Dictionary<long, CellView>();
    private HashSet<long> ids = new HashSet<long>();
    private HashSet<long> idsToRemove = new HashSet<long>();

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
        ids.Clear();
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


        idsToRemove.Clear();
        // Prepare list of not existing cells
        foreach (var entityId in cellViews.Keys)
        {
            if (ids.Contains(entityId) == false)
            {
                idsToRemove.Add(entityId);
            }
        }

        // Remove not existing cells
        foreach (var entityId in idsToRemove)
        {
            EntityView view = cellViews[entityId];
            cellViews.Remove(entityId);
            Destroy(view.gameObject);
        }
    }

    private void FollowTarget(Transform target)
    {
        CameraFollowTarget follow = Camera.main.GetComponent<CameraFollowTarget>();
        follow.SetTarget(target, false);
    }
}
