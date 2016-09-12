using UnityEngine;
using System.Collections;

public class EntityView : MonoBehaviour
{
    public EntityData Data { get; private set; }

    public virtual void BindData(EntityData data)
    {
        Data = data;
    }
}
