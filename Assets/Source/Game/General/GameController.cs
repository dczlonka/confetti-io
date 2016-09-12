using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : Singleton<GameController>
{
    public GameView GameView { get; private set; }
    public GameModel GameModel { get; private set; }
    private Communication communication;

    public void StartGame(RoomData room, PlayerData myPlayer)
    {
        if (room == null)
        {
            StopGame();
            throw new UnityException("Trying to start but there's no room!");
        }

        if (myPlayer == null)
        {
            StopGame();
            throw new UnityException("Trying to start but player is missing!");
        }

        GameModel = new GameModel(room, myPlayer);
        GameView = GetComponent<GameView>();
        GameView.CreateViews(GameModel.room.cells);

        communication = new Communication(this);
        communication.StartSyncLoop();
    }

    public void StopGame()
    {
        communication.StopSyncLoop();
    }

    public void UpdateCells(List<CellData> cells)
    {
        GameModel.room.cells.Clear();
        GameModel.room.cells = cells;
        GameView.UpdateViews(cells);
    }
}
