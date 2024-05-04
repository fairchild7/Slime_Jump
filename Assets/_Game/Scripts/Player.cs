using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.TAG_POINT_COLLIDER))
        {
            Debug.Log("Point++");
            collision.gameObject.SetActive(false);
        }
    }
}
