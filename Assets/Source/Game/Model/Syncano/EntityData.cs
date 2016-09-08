using UnityEngine;
using System.Collections;
using Syncano;

/// <summary>
/// Base for all objects in game having position.
/// </summary>
[System.Serializable]
public class EntityData : SyncanoObject
{
    /// <summary>
    /// Position on x axis.
    /// </summary>
    public float x;

    /// <summary>
    /// Position on y axis.
    /// </summary>
    public float y;
}
