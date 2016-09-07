using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
{
    public const float MAP_WIDTH = 25;
    public const float MAP_HEIGHT = 25;
    public const float COLLIDER_THICKNESS = 0.2f;

    void Start()
    {
        
    }

//    private void GenerateBorders()
//    {
//        // Left
//        GameObject left = new GameObject("Left Border");
//        left.transform.position = new Vector3(-MAP_WIDTH / 2 - COLLIDER_THICKNESS / 2, 0, 0);
//        left.transform.SetParent(transform, true);
//        BoxCollider2D leftCollider = left.AddComponent<BoxCollider2D>();
//        left.transform.localScale = new Vector3(COLLIDER_THICKNESS, MAP_HEIGHT, 1);
//
//        // Right
//        GameObject right = new GameObject("Right Border");
//        right.transform.position = new Vector3(MAP_WIDTH / 2 + COLLIDER_THICKNESS / 2, 0, 0);
//        right.transform.SetParent(transform, true);
//        BoxCollider2D rightCollider = right.AddComponent<BoxCollider2D>();
//        rightCollider.size = new Vector3(COLLIDER_THICKNESS, MAP_HEIGHT, 0);
//
//        // Top
//        GameObject top = new GameObject("Top Border");
//        top.transform.position = new Vector3(0, MAP_HEIGHT / 2 + COLLIDER_THICKNESS / 2, 0);
//        top.transform.SetParent(transform, true);
//        BoxCollider2D topCollider = top.AddComponent<BoxCollider2D>();
//        topCollider.size = new Vector3(MAP_WIDTH + COLLIDER_THICKNESS * 2, COLLIDER_THICKNESS, 0);
//
//        // Bottom
//        GameObject bottom = new GameObject("Bottom Border");
//        bottom.transform.position = new Vector3(0, -MAP_HEIGHT / 2 - COLLIDER_THICKNESS / 2, 0);
//        bottom.transform.SetParent(transform, true);
//        BoxCollider2D bottomCollider = bottom.AddComponent<BoxCollider2D>();
//        bottomCollider.size = new Vector3(MAP_WIDTH + COLLIDER_THICKNESS * 2, COLLIDER_THICKNESS, 0);
//
//    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(MAP_WIDTH, MAP_HEIGHT, 0.1f));
    }
}
