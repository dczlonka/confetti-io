﻿using UnityEngine;
using System.Collections;
using Syncano.Data;

public class CellView : EntityView
{
    public const float MAX_SPEED = 2.5f;
    public const float ACCELERATION = 5f;

    private CellData CellData { get { return Data as CellData; } }
    private Rigidbody2D rigid;
    private float speed = 0;
    private GameModel model;

	void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
        model = GameController.Instance.GameModel;
	}
	
    public override void BindData (EntityData data)
    {
        base.BindData (data);
        gameObject.name = "Cell " + data.id;
    }

	void Update ()
    {
        if (CellData != null)
        {
            if (IsMyCell())
            {
                if (GameInput.Axis != Vector3.zero)
                {
                    // Accelerate
                    speed = Mathf.Lerp(speed, MAX_SPEED, ACCELERATION * Time.deltaTime);
                    rigid.velocity = GameInput.Axis * speed;
                }
                else
                {
                    // Deccelerate
                    speed = Mathf.Lerp(speed, 0, ACCELERATION * Time.deltaTime);
                    rigid.velocity = rigid.velocity.normalized * speed;
                }

                CellData.x = transform.position.x;
                CellData.y = transform.position.y;
            }
            else
            {
                rigid.MovePosition(new Vector3(Data.x, Data.y, 0));
            }
        }
	}

    private bool IsMyCell()
    {
        return CellData.OwnerId != 0 && CellData.OwnerId == model.PlayerId;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        CellView cell = other.GetComponent<CellView>();

        if (cell != null && Data.id > cell.Data.id) // It will make it call only on one collider side.
        {
            if (cell.CellData.size != CellData.size)
            {
                GameController.Instance.Communication.TryEatCell(Data.id, cell.Data.id);
            }
        }
    }
}
