using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class TileStepController : MonoBehaviour
{
    [SerializeField] private float _step;

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        ClampSpriteSize();
    }

#endif

    private void ClampSpriteSize()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            spriteRenderer.size = new Vector2(RoundToStep(spriteRenderer.size.x, _step),
                                               RoundToStep(spriteRenderer.size.y, _step));

    }

    private float RoundToStep(float value, float step)
    {
        if (step <= 0)
            return value;

        return Mathf.Round(value / step) * step;
    }
}
