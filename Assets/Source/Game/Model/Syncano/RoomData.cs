using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Syncano;

/// <summary>
/// Represents room where game is going on.
/// </summary>
[System.Serializable]
public class RoomData : SyncanoObject
{
    /// <summary>
    /// Players participating in game.
    /// </summary>
    public List<PlayerData> players;

    /// <summary>
    /// All cells that belong to players.
    /// </summary>
    public List<CellData> cells;
}
