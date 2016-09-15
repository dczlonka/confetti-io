using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModel
{
    public long RoomId { get; private set; }
    public long PlayerId { get { return Player != null ? Player.id : 0; } }

    public List<CellData> Cells { get; private set; }
    public List<FoodData> Food { get; private set; }
    public PlayerData Player { get; private set; }

    public CellData MainCell { get; private set; } // Cell to controll.
    public List<CellData> MyCells { get; private set; } // All my player cells.

    public GameModel(long roomId)
    {
        this.RoomId = roomId;
    }

    public void SetMyPlayer(PlayerData player)
    {
        Player = player;
    }

    public void UpdateCells(List<CellData> allCells)
    {
        Cells = allCells;
        if (Player != null)
        {
            SetMyCells(Player.id, allCells);
            MainCell = PickMainCell(Player.id, MyCells);
        }
    }

    public void UpdateFood(List<FoodData> allFood)
    {
        Food = allFood;
    }

    public CellData PickMainCell(long playerId, List<CellData> myCells)
    {
        foreach (var item in myCells)
        {
            if (item.OwnerId == playerId)
                return item;
        }

        return null;
    }

    public void SetMyCells(long playerId, List<CellData> allCells)
    {
        if (MyCells == null)
            MyCells = new List<CellData>();
        else
            MyCells.Clear();

        if (allCells == null)
            return;

        foreach (var item in allCells)
        {
            if (item.OwnerId == playerId)
            {
                MyCells.Add(item);
            }
        }
    }
}
