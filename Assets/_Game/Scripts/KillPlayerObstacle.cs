using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerObstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.TAG_PLAYER))
        {
            Debug.Log("Kill!");
        }
    }
}
