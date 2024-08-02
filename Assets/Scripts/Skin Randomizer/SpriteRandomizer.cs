using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteRandomizer : MonoBehaviour
{
    [SerializeField] private Sprite[] _spriteVariants;
    [SerializeField] private SpriteRenderer[] _copyTo;

    public void RandomizeSprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null || _spriteVariants == null || _spriteVariants.Length == 0)
            return;

        Sprite randomSprite = _spriteVariants[Random.Range(0, _spriteVariants.Length)];

        spriteRenderer.sprite = randomSprite;
        foreach (SpriteRenderer otherRenderer in _copyTo)
            if (otherRenderer != null)
                otherRenderer.sprite = randomSprite ;
        
        //Debug.Log(randomSprite);
    }
}
