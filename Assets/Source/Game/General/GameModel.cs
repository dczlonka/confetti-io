using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModel
{
    public RoomData room { get; private set; }
    public PlayerData myPlayer { get; private set; }

    public CellData mainCell { get; private set; } // Cell to controll.
    public List<CellData> myCells { get; private set; } // All my player cells.

    public GameModel(RoomData room, PlayerData myPlayer)
    {
        this.room = room;
        this.myPlayer = myPlayer;
        PickMainCell(myPlayer.id);
        SetMyCells(room.cells);
    }

    public CellData PickMainCell(long playerId)
    {
        foreach (var item in room.cells)
        {
            if (item.ownerId == playerId)
                return item;
        }

        return null;
    }

    public void SetMyCells(List<CellData> allCells)
    {
        if (myCells == null)
            myCells = new List<CellData>();
        else
            myCells.Clear();

        if (allCells == null)
            return;

        foreach (var item in allCells)
        {
            if (item.ownerId == myPlayer.id)
            {
                allCells.Add(item);
            }
        }
    }
}
