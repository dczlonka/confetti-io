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
    private int size;

	void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
        model = GameController.Instance.GameModel;
	}
	
    public override void BindData (EntityData data)
    {
        base.BindData (data);
        gameObject.name = "Cell " + data.id;

        if (size != CellData.size)
        {
            size = CellData.size;
            Resize();
        }
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

    private Coroutine resizeRoutine;
    private void Resize()
    {
        if (resizeRoutine != null)
            StopCoroutine(resizeRoutine);

        resizeRoutine = StartCoroutine(ResizeRoutine());
    }

    private IEnumerator ResizeRoutine()
    {
        float scale = Size2Scale(size);
        Vector3 newScale = new Vector3(scale, scale, scale);

        while (Vector3.Distance(transform.localScale, newScale) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, newScale, Time.deltaTime * 10);
            yield return null;
        }

        resizeRoutine = null;
    }

    private float Size2Scale(int size)
    {
        if (size < 5)
            return 0.5f;

        return (float)size / 10.0f;
    }
}
