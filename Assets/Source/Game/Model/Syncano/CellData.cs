using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

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

    [JsonIgnore]
    public long OwnerId
    {
        get { return ownerId != null ? ownerId.value : 0; }
    }

	public CellData () { }

    /// <summary>
    /// Identifier of the owner Player..
    /// </summary>
	[JsonProperty("ownerId")]
	public SyncanoReference ownerId { get; set; }

    /// <summary>
    /// Size of the cell.
    /// </summary>
    public int size;
}
