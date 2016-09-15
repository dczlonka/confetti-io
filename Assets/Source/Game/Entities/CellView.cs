using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Syncano.Data;

public class CellView : EntityView
{
    public const float MAX_SPEED = 2.5f;
    public const float ACCELERATION = 5f;

    [SerializeField]
    private Text nicknameLabel;

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
            nicknameLabel.text = "id: " + CellData.id.ToString() + "\nsize: " + CellData.size.ToString();

            if (IsMyCell(CellData))
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

    private bool IsMyCell(CellData cell)
    {
        return cell.OwnerId != 0 && cell.OwnerId == model.PlayerId;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        CellView cell = other.GetComponent<CellView>();

        if (cell != null && Data.id > cell.Data.id) // It will make it call only on one collider side.
        {
            if (IsMyCell(CellData) || IsMyCell(cell.CellData)) // Should be invoked only by cell owners.
            {
                if (cell.CellData.size != CellData.size)
                {
                    GameController.Instance.Communication.TryEatCell(Data.id, cell.Data.id);
                }
            }

            return; // Don't check food
        }

        FoodView food = other.GetComponent<FoodView>();

        if (food != null && food.Data != null && IsMyCell(CellData))
        {
            GameController.Instance.Communication.TryEatFood(Data.id, food.Data.id);
        }
    }
}
