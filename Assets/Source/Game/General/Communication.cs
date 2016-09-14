using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Communication
{
    private GameController controller;
    private GameModel model;
    private List<CellData> syncCells = new List<CellData>();

    private bool isRunning;
    private Coroutine syncLoop;

    public Communication (GameController controller)
    {
        this.controller = controller;
    }

    public void StartSyncLoop(GameModel model)
    {
        this.model = model;
        isRunning = true;
        syncLoop = controller.StartCoroutine(SyncLoop());
        GetCells();
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
}
