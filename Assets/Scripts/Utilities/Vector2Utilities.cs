using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Utilities
{
    public static Vector2 Rotate(this Vector2 original, float angle)
    {
        float angleRadians = angle * Mathf.Deg2Rad;
        Vector2 rotatedVector = new Vector2(original.x * Mathf.Cos(angleRadians) - original.y * Mathf.Sin(angleRadians),
                                            original.x * Mathf.Sin(angleRadians) + original.y * Mathf.Cos(angleRadians));
        return rotatedVector;
    }
}
