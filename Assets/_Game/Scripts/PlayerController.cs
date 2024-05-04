using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform tf;
    [SerializeField] Rigidbody2D rb;

    [Header("Move properties")]
    [SerializeField] float minimumJumpForce = 5f;
    [SerializeField] float maximumJumpForce = 20f;
    [SerializeField] float maximumChargedTime = 3f;

    private float timer = 0f;
    private float jumpForce = 0f;

    [SerializeField] Vector2 jumpVector = new Vector2(2, 1);

    [Header("States properties")]
    [SerializeField] float YPosToConsiderDead = 0f;

    public bool IsDead = false;

    private void Update()
    {
        Debug.DrawLine(tf.position, (Vector2)tf.position + jumpVector.normalized);
        timer += GameManager.DeltaTime;

        IsDead = CheckDeadCondition();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            timer = 0f;            
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpForce = minimumJumpForce + Mathf.Clamp(timer / maximumChargedTime, 0f, 1f) * (maximumJumpForce - minimumJumpForce);
            rb.AddForce(jumpVector.normalized * jumpForce, ForceMode2D.Impulse);
        }
    }

    public bool CheckDeadCondition()
    {
        if (tf.position.y < YPosToConsiderDead)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
