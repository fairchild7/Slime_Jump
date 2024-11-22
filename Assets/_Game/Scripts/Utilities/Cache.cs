using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public static class Cache
{
    private static Dictionary<Collider2D, PointCollider> pointColliders = new Dictionary<Collider2D, PointCollider>();

    public static PointCollider GetPointCollider(Collider2D collider)
    {
        if (!pointColliders.ContainsKey(collider))
        {
            PointCollider pointCollider = collider.GetComponent<PointCollider>();
            pointColliders.Add(collider, pointCollider);
        }
        return pointColliders[collider];
    }

    //private static Dictionary<Collider, Bullet> bullets = new Dictionary<Collider, Bullet>();

    //public static Bullet GetBullet(Collider collider)
    //{
    //    if (!bullets.ContainsKey(collider))
    //    {
    //        Bullet bullet = collider.GetComponent<Bullet>();
    //        bullets.Add(collider, bullet);
    //    }
    //    return bullets[collider];
    //}
}
