using UnityEngine;
using System.Collections;
using Syncano;

/// <summary>
/// Player data.
/// </summary>
[System.Serializable]
public class PlayerData : SyncanoObject
{
    /// <summary>
    /// The player nick name.
    /// </summary>
    public string nick;

    /// <summary>
    /// Inactive player and his cells should not take place in game.
    /// </summary>
    public bool isAlive;

    /// <summary>
    /// Collection of cells that belong to this player.
    /// </summary>
    public CellData[] cells;
}
