using UnityEngine;
using System.Collections;
using Syncano;

/// <summary>
/// Base for all objects in game having position.
/// </summary>
[System.Serializable]
public class EntityData : SyncanoObject
{
    public virtual string ViewResource
    {
        get { return "/Views/"; }
    }

    public EntityView View { get; private set; }

    /// <summary>
    /// Position on x axis.
    /// </summary>
    public float x;

    /// <summary>
    /// Position on y axis.
    /// </summary>
    public float y;

    public EntityView CreateView()
    {
        GameObject go = Resources.Load(ViewResource) as GameObject;
        if (go == null)
            throw new UnityException("View not found: " + ViewResource);

        EntityView ev = go.GetComponent<EntityView>();
        if (ev == null)
            throw new UnityException("EntityView component not found: " + ViewResource);

        View = ev;
        ev.BindData(this);
        return ev;
    }
}
