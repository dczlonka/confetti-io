using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Syncano.Data;

public class Communication
{
    public const float PLAYER_HEARTBEAT_INTERVAL = 60; // 60 seconds

    private GameController controller;
    private GameModel model;
    private List<CellData> syncCells = new List<CellData>();

    private bool isRunning;
    private Coroutine syncLoop;
    private Coroutine playerHeartbeat;

    public Communication (GameController controller)
    {
        this.controller = controller;
    }

    public void StartSyncLoop(GameModel model)
    {
        this.model = model;
        isRunning = true;
        syncLoop = controller.StartCoroutine(SyncLoop());
        playerHeartbeat = controller.StartCoroutine(PlayerHeartbeat());
        GetCells();
    }

    public void StopSyncLoop()
    {
        isRunning = false;

        if (syncLoop != null)
        {
            controller.StopCoroutine(syncLoop);
            controller.StopCoroutine(playerHeartbeat);
            syncLoop = null;
            playerHeartbeat = null;
        }
    }

    private IEnumerator SyncLoop()
    {
        while (isRunning)
        {
            yield return SendMyCellPositions();
        }
    }

    private IEnumerator PlayerHeartbeat()
    {
        while (isRunning)
        {
            if (model.Player != null)
            {
                model.Player.revisionCounter++; // Increment to change updated at date.
                yield return SyncanoWrapper.Please().Save(model.Player, OnPlayerSaved);
                yield return new WaitForSeconds(PLAYER_HEARTBEAT_INTERVAL);
            }
            else
            {
                yield return null;
            }
        }
    }

    //--------------------------- Send my cells ---------------------------//

    public Coroutine SendMyCellPositions()
    {
        return controller.StartCoroutine(SendCellPositions(model.MyCells));
    }

    private IEnumerator SendCellPositions(List<CellData> cells)
    {
        if (cells != null)
        {
            syncCells.AddRange(cells);
            foreach (var item in syncCells)
            {
                yield return SyncanoWrapper.Please().Save(item, null);
            }
            syncCells.Clear();
        }
    }

    //--------------------------- Get cells ---------------------------//

    private void GetCells()
    {
        SyncanoWrapper.Please().Get<CellData>(OnCellsDownloaded);
    }

    private void OnCellsDownloaded(ResponseGetList<CellData> response)
    {
        if (!isRunning)
            return;

        if (response.IsSuccess)
            controller.UpdateCells(response.Objects);


        GetCells(); // Try update again.
    }

    //--------------------------- Player heartbeat ---------------------------//

    private void OnPlayerSaved(Response<PlayerData> response)
    {
        if (!isRunning)
            return;

        if (response.IsSuccess)
            model.Player.revisionCounter = response.Data.revisionCounter;
    }
}
