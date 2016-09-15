using UnityEngine;
using System.Collections;

public class FoodView : EntityView
{
    [SerializeField]
    private SpriteRenderer sprite;

    void Start()
    {
        // Pick a random, saturated and not-too-dark color
        sprite.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    public override void BindData (EntityData data)
    {
        base.BindData (data);
        gameObject.name = "Food " + data.id;
    }
}
