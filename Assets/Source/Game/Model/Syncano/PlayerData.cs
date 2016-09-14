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
    /// We need to increment it to change updated at date.
    /// </summary>
    public int revisionCounter;
}
