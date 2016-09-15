using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Syncano.Data;
using Newtonsoft.Json;

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
        GetFood();
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

    //--------------------------- Get players ---------------------------//

    public void GetPlayers()
    {
        Dictionary<string, string> payload = new Dictionary<string, string>();
        payload.Add("room_id", model.RoomId.ToString());
        SyncanoWrapper.Please().CallScriptEndpoint(Constants.ENDPOINT_GET_ALL_PLAYERS_ID, Constants.ENDPOINT_GET_ALL_PLAYERS, OnPlayersDownloaded, payload);
    }

    private void OnPlayersDownloaded(ScriptEndpoint response)
    {
        if (!isRunning)
            return;

        if (response.IsSuccess)
            Debug.Log(response.stdout);
            //controller.UpdateCells(response.Objects);
    }

    //--------------------------- Get food ---------------------------//

    public void GetFood()
    {
        Dictionary<string, string> payload = new Dictionary<string, string>();
        payload.Add("room_id", model.RoomId.ToString());
        SyncanoWrapper.Please().CallScriptEndpoint(Constants.ENDPOINT_GET_FOOD_ID, Constants.ENDPOINT_GET_FOOD, OnFoodsDownloaded, payload);
    }

    private void OnFoodsDownloaded(ScriptEndpoint response)
    {
        if (!isRunning)
            return;

        if (response.IsSuccess)
        {
            List<FoodData> food = DeserializeJson<List<FoodData>>(response.stdout);
            controller.UpdateFood(food);
        }

        GetFood();
    }

    //--------------------------- Player heartbeat ---------------------------//

    private void OnPlayerSaved(Response<PlayerData> response)
    {
        if (!isRunning)
            return;

        if (response.IsSuccess)
            model.Player.revisionCounter = response.Data.revisionCounter;
    }

    //--------------------------- Eat cell ---------------------------//

    private List<KeyValuePair<long, long>> cellsToEat = new List<KeyValuePair<long, long>>();
    private KeyValuePair<long, long> currentlyEating;
    private bool isEating;

    public void TryEatCell(long cellA, long cellB)
    {
        if (!isRunning)
            return;

        if (CellsToEatContainsPair(cellA, cellB))
        {
            return;
        }
        else
        {
            cellsToEat.Add(new KeyValuePair<long, long>(cellA, cellB));
        }

        PickAndEat();
    }

    private void PickAndEat()
    {
        if (isEating == false && cellsToEat.Count > 0)
        {
            isEating = true;
            currentlyEating = cellsToEat[0];

            Dictionary<string, string> payload = new Dictionary<string, string>();
            payload.Add("player_cell_id", currentlyEating.Key.ToString());
            payload.Add("cell_id", currentlyEating.Value.ToString());
            payload.Add("room_id", model.RoomId.ToString());

            SyncanoWrapper.Please().CallScriptEndpoint(Constants.ENDPOINT_TRY_EAT_CELL_ID, Constants.ENDPOINT_TRY_EAT_CELL, OnTryEatCellFinished, payload);
        }
    }

    private void OnTryEatCellFinished(ScriptEndpoint response)
    {
        cellsToEat.Remove(currentlyEating);
        isEating = false;
        PickAndEat();
    }

    private bool CellsToEatContainsPair(long cellA, long cellB)
    {
        foreach (var item in cellsToEat)
        {
            if ((item.Key == cellA && item.Value == cellB) || (item.Key == cellB && item.Value == cellA))
            {
                return true;
            }
        }

        return false;
    }

    //--------------------------- Eat food ---------------------------//

    private List<KeyValuePair<long, long>> foodToEat = new List<KeyValuePair<long, long>>();
    private KeyValuePair<long, long> currentlyEatingFood;
    private bool isEatingFood;

    public void TryEatFood(long cellId, long foodId)
    {
        if (!isRunning)
            return;

        if (FoodToEatContainsPair(cellId, foodId))
        {
            return;
        }
        else
        {
            foodToEat.Add(new KeyValuePair<long, long>(cellId, foodId));
        }

        PickFoodAndEat();
    }

    private void PickFoodAndEat()
    {
        if (isEating == false && cellsToEat.Count > 0)
        {
            isEatingFood = true;
            currentlyEatingFood = foodToEat[0];

            Dictionary<string, string> payload = new Dictionary<string, string>();
            payload.Add("cell_id", currentlyEating.Key.ToString());
            payload.Add("food_id", currentlyEating.Value.ToString());
            payload.Add("room_id", model.RoomId.ToString());

            SyncanoWrapper.Please().CallScriptEndpoint(Constants.ENDPOINT_TRY_EAT_CELL_ID, Constants.ENDPOINT_TRY_EAT_CELL, OnTryEatFoodFinished, payload);
        }
    }

    private void OnTryEatFoodFinished(ScriptEndpoint response)
    {
        foodToEat.Remove(currentlyEatingFood);
        isEatingFood = false;
        PickFoodAndEat();
    }

    private bool FoodToEatContainsPair(long cellId, long foodId)
    {
        foreach (var item in foodToEat)
        {
            if ((item.Key == cellId && item.Value == foodId))
            {
                return true;
            }
        }

        return false;
    }

    //--------------------------- Tools ---------------------------//

    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }
}
