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
        get { return "Views/"; }
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
        GameObject prefab = Resources.Load<GameObject>(ViewResource);
        if (prefab == null)
            throw new UnityException("View not found: " + ViewResource);

        GameObject go = (GameObject) GameObject.Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);

        EntityView ev = go.GetComponent<EntityView>();
        if (ev == null)
            throw new UnityException("EntityView component not found: " + ViewResource);

        View = ev;
        ev.BindData(this);
        return ev;
    }
}
