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
    private bool isIncreasingForce = true;
    private bool isCharging = false;
    private float timerNormalized = 0f;
    private float timerSnapshot = 0f;
    private Vector2 positionSnapshot;

    [SerializeField] Vector2 jumpVector = new Vector2(2, 1);

    [Header("States properties")]
    [SerializeField] float YPosToConsiderDead = 0f;

    private bool isGrounded = true;

    public bool IsDead = false;

    private void Update()
    {
        this.RegisterListener(EventID.OnSteppingOnPlatform, (param) =>
        {
            isGrounded = true;
        });

        if (isCharging)
        {
            timerNormalized = timer / maximumChargedTime;
        }
        else
        {
            if (isGrounded)
            {
                timerNormalized = 0f;
            }
            else
            {
                timerNormalized = timerSnapshot / maximumChargedTime;
            }
        }
        this.PostEvent(EventID.OnChangeJumpForce, timerNormalized);
        
        Debug.DrawLine(tf.position, (Vector2)tf.position + jumpVector.normalized);

        if (timer >= maximumChargedTime)
        {
            isIncreasingForce = false;
        }
        else if (timer < 0f)
        {
            isIncreasingForce = true;
        }

        timer += GameManager.DeltaTime * (isIncreasingForce ? 1 : -1);

        IsDead = CheckDeadCondition();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isGrounded)
            {
                return;
            }
            isCharging = true;
            timer = 0f;            
        }
        if (Input.GetKeyUp(KeyCode.Space) && isCharging)
        {
            if (!isGrounded)
            {
                return;
            }
            isCharging = false;
            isGrounded = false;
            timerSnapshot = timer;
            positionSnapshot = tf.position;
            jumpForce = minimumJumpForce + Mathf.Clamp(timer / maximumChargedTime, 0f, 1f) * (maximumJumpForce - minimumJumpForce);
            rb.AddForce(jumpVector.normalized * jumpForce, ForceMode2D.Impulse);
        }

        //temp
        if (IsDead)
        {
            this.PostEvent(EventID.OnPlayerDead);
            tf.position = positionSnapshot;
            isGrounded = true;
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
