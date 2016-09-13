using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModel
{
    public long RoomId { get; private set; }

    /// <summary>
    /// All cells that belong to players.
    /// </summary>
    public List<CellData> Cells { get; private set; }
    public PlayerData MyPlayer { get; private set; }

    public CellData MainCell { get; private set; } // Cell to controll.
    public List<CellData> MyCells { get; private set; } // All my player cells.

    public GameModel(long roomId, PlayerData myPlayer)
    {
        this.RoomId = roomId;
        this.MyPlayer = myPlayer;
    }

    public void UpdateCells(List<CellData> allCells)
    {
        Cells = allCells;
        SetMyCells(allCells);
        MainCell = PickMainCell(MyPlayer.id, MyCells);
    }

    public CellData PickMainCell(long playerId, List<CellData> myCells)
    {
        foreach (var item in myCells)
        {
            if (item.ownerId == playerId)
                return item;
        }

        return null;
    }

    public void SetMyCells(List<CellData> allCells)
    {
        if (MyCells == null)
            MyCells = new List<CellData>();
        else
            MyCells.Clear();

        if (allCells == null)
            return;

        foreach (var item in allCells)
        {
            if (item.ownerId == MyPlayer.id)
            {
                MyCells.Add(item);
            }
        }
    }
}
