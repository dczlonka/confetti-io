using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : Singleton<GameController>
{
    public GameView GameView { get; private set; }
    public GameModel GameModel { get; private set; }
    public bool IsRunning { get; private set; }
    private Communication communication;

    public void StartGame(long roomId)
    {
        if (IsRunning)
        {
            Debug.LogWarning("Game is already running!");
        }
        else
        {
            if (roomId == 0)
            {
                StopGame();
                throw new UnityException("Trying to start but there's no room!");
            }

            IsRunning = true;
            GameModel = new GameModel(roomId);
            GameView = GetComponent<GameView>();

            communication = new Communication(this);
            communication.StartSyncLoop(GameModel);
        }
    }

    public void Join(PlayerData myPlayer)
    {
        if (myPlayer != null && myPlayer.id != 0)
        {
            GameModel.SetMyPlayer(myPlayer);
        }
    }

    public void StopGame()
    {
        if (IsRunning)
        {
            communication.StopSyncLoop();
        }
        else
        {
            Debug.LogWarning("Game already stopped!");
        }
    }

    public void UpdateCells(List<CellData> cells)
    {
        GameModel.UpdateCells(cells);
        GameView.UpdateViews(cells);
    }
}
