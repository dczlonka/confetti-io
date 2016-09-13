using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : Singleton<GameController>
{
    public GameView GameView { get; private set; }
    public GameModel GameModel { get; private set; }
    private Communication communication;

    public void StartGame(long roomId, PlayerData myPlayer)
    {
        if (roomId == 0)
        {
            StopGame();
            throw new UnityException("Trying to start but there's no room!");
        }

        if (myPlayer == null || myPlayer.id == 0)
        {
            StopGame();
            throw new UnityException("Trying to start but player is missing!");
        }

        GameModel = new GameModel(roomId, myPlayer);
        GameView = GetComponent<GameView>();

        communication = new Communication(this);
        communication.StartSyncLoop();
    }

    public void StopGame()
    {
        communication.StopSyncLoop();
    }

    public void UpdateCells(List<CellData> cells)
    {
        GameModel.UpdateCells(cells);
        GameView.UpdateViews(cells);
    }
}
