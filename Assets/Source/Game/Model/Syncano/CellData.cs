using UnityEngine;
using System.Collections;

/// <summary>
/// Data representing single cell.
/// </summary>
[System.Serializable]
public class CellData : EntityData
{
    public override string ViewResource
    {
        get { return base.ViewResource + "CellView"; }
    }

    /// <summary>
    /// Identifier of the owner Player..
    /// </summary>
    public long ownerId;

    /// <summary>
    /// Size of the cell.
    /// </summary>
    public int size;
}
