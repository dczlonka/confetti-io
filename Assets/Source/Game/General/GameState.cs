﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    private RoomData room;
    private PlayerData myPlayer;

    private CellData mainCell; // Cell to controll.
    private List<CellData> myCells; // All my player cells.

    private bool isRunning = false;
    private Coroutine syncLoop;

    public void StartGame(RoomData room, PlayerData myPlayer)
    {
        this.room = room;
        this.myPlayer = myPlayer;
        this.mainCell = PickMainCell(myPlayer.id);

        TryToStartSyncLoop();
    }

    public void StopGame()
    {
        room = null;
        myPlayer = null;
        mainCell = null;
        myCells.Clear();
        isRunning = false;

        if (syncLoop != null)
        {
            StopCoroutine(syncLoop);
            syncLoop = null;
        }
    }

    private void TryToStartSyncLoop()
    {
        if (room == null)
        {
            StopGame();
            throw new UnityException("Trying to start but there's no room!");
        }

        if (myPlayer == null)
        {
            StopGame();
            throw new UnityException("Trying to start but my player is missing!");
        }


        isRunning = true;
        syncLoop = StartCoroutine(UpdateLoop());
    }

    private IEnumerator UpdateLoop()
    {
        while (isRunning)
        {
            yield return SendMyCellPositions();
        }
    }

    private CellData PickMainCell(long playerId)
    {
        foreach (var item in room.cells)
        {
            if (item.ownerId == playerId)
                return item;
        }

        return null;
    }

    private void SetMyCells(List<CellData> allCells)
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

    public Coroutine SendMyCellPositions()
    {
        return StartCoroutine(SendCellPositions(myCells));
    }

    private IEnumerator SendCellPositions(List<CellData> cells)
    {
        foreach (var item in room.cells)
        {
            yield return SyncanoWrapper.Please().Save(item, null);
        }
    }
}
