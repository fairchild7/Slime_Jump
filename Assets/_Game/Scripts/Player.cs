using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Platform currentStandingPlatform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.TAG_POINT_COLLIDER))
        {
            PointCollider pointCollider = Cache.GetPointCollider(collision);

            if (currentStandingPlatform != null && currentStandingPlatform != pointCollider.Platform)
            {
                currentStandingPlatform.IsDestroyable = true;
            }
            currentStandingPlatform = pointCollider.Platform;

            if (!pointCollider.IsClaimed)
            {
                this.PostEvent(EventID.OnSteppingOnNewPlatform);
                pointCollider.IsClaimed = true;
            }
            //else
            //{
            //    if (pointCollider.Platform.IsShaking)
            //    {

            //    }
            //    else
            //    {
            //        pointCollider.Platform.Shake();
            //    }
            //}
            this.PostEvent(EventID.OnSteppingOnPlatform);
        }
    }
}
