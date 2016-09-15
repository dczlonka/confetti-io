using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameView : MonoBehaviour
{
    private Dictionary<long, CellView> cellViews = new Dictionary<long, CellView>();
    private Dictionary<long, FoodView> foodViews = new Dictionary<long, FoodView>();

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

    private void AddView(FoodData data)
    {
        EntityView view = data.CreateView();
        foodViews.Add(data.id, view as FoodView);
    }

    public void UpdateViews(List<CellData> cells)
    {
        HashSet<long> ids = new HashSet<long>();
        HashSet<long> idsToRemove = new HashSet<long>();

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

    public void UpdateViews(List<FoodData> foods)
    {
        HashSet<long> ids = new HashSet<long>();
        HashSet<long> idsToRemove = new HashSet<long>();

        foreach (var food in foods)
        {
            if (foodViews.ContainsKey(food.id))
            {
                // Update
                FoodView view = foodViews[food.id];
                view.BindData(food);
            }
            else
            {
                // Add new
                AddView(food);
            }

            ids.Add(food.id);
        }

        // Prepare list of not existing cells
        foreach (var entityId in foodViews.Keys)
        {
            if (ids.Contains(entityId) == false)
            {
                idsToRemove.Add(entityId);
            }
        }

        // Remove not existing cells
        foreach (var entityId in idsToRemove)
        {
            EntityView view = foodViews[entityId];
            foodViews.Remove(entityId);
            Destroy(view.gameObject);
        }
    }

    private void FollowTarget(Transform target)
    {
        CameraFollowTarget follow = Camera.main.GetComponent<CameraFollowTarget>();
        follow.SetTarget(target, false);
    }
}
