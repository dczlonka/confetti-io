using UnityEngine;
using System.Collections;

/// <summary>
/// Represents food objects.
/// </summary>
[System.Serializable]
public class FoodData : EntityData
{
    public override string ViewResource
    {
        get { return base.ViewResource + "FoodView"; }
    }
}
