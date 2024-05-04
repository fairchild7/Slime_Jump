using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PointCollider : MonoBehaviour
{
    private int _id;
    public int Id => _id;

    public void AddPointCollider(int width, int height, int id)
    {
        transform.localPosition = new Vector3(0f, height, 0f);
        gameObject.AddComponent<BoxCollider2D>();
        GetComponent<BoxCollider2D>().size = new Vector2(width - 0.1f, 0.01f);
        GetComponent<BoxCollider2D>().isTrigger = true;
        tag = Constants.TAG_POINT_COLLIDER;
        _id = id;
    }
}
