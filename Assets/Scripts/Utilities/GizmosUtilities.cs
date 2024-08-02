using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class GizmosUtilities
{
    public static void DrawCapsule(Vector2 point, Vector2 size)
    {
        if (size.x > size.y)
        {
            float d = size.x - size.y;
            Gizmos.DrawWireSphere(point + Vector2.right * d, size.y);
            Gizmos.DrawWireSphere(point + Vector2.left * d, size.y);
            Gizmos.DrawLine(point + new Vector2(-d, size.y), point + new Vector2(d, size.y));
            Gizmos.DrawLine(point + new Vector2(-d, -size.y), point + new Vector2(d, -size.y));
        }
        else
        {
            float d = size.y - size.x;
            Gizmos.DrawWireSphere(point + Vector2.down * d, size.x);
            Gizmos.DrawWireSphere(point + Vector2.up * d, size.x);
            Gizmos.DrawLine(point + new Vector2(size.x, -d), point + new Vector2(size.x, d));
            Gizmos.DrawLine(point + new Vector2(-size.x, -d), point + new Vector2(-size.x, d));
        }
    }
}
