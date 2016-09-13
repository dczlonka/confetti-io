using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Communication
{
    private GameController controller;
    private long roomId;
    private long myPlayerId;

    private bool isRunning;
    private Coroutine syncLoop;

    public Communication (GameController controller)
    {
        this.controller = controller;
    }

    public void StartGame(long roomId, long myPlayerId)
    {
        this.roomId = roomId;
        this.myPlayerId = myPlayerId;
        StartSyncLoop();
    }

    public void StopSyncLoop()
    {
        isRunning = false;

        if (syncLoop != null)
        {
            controller.StopCoroutine(syncLoop);
            syncLoop = null;
        }
    }

    public void StartSyncLoop()
    {
        isRunning = true;
        syncLoop = controller.StartCoroutine(SyncLoop());
        GetCells();
    }

    private IEnumerator SyncLoop()
    {
        while (isRunning)
        {
            yield return SendMyCellPositions();
        }
    }

    //--------------------------- Send my cells ---------------------------//

    public Coroutine SendMyCellPositions()
    {
//        if (controller.GameModel.MyCells != null)
//            Debug.Log("Send: " + controller.GameModel.MyCells.Count);

        return controller.StartCoroutine(SendCellPositions(controller.GameModel.MyCells));
    }

    private IEnumerator SendCellPositions(List<CellData> cells)
    {
        if (cells != null)
        {
            foreach (var item in cells)
            {
                yield return SyncanoWrapper.Please().Save(item, null);
            }
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


        //GetCells(); // Try update again.
    }
}
