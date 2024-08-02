using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinRandomizer : MonoBehaviour
{
    [SerializeField] private bool _randomizeOnStart;

    [Header("Optional")]
    [SerializeField] private GameObject[] _toggleableElements;

    private void Reset()
    {
        
    }

    private void Start()
    {
        if (_randomizeOnStart)
            RandomizeSkin();
    }

    public void RandomizeSkin()
    {
        SpriteRandomizer[]  spriteRandomizers = GetComponentsInChildren<SpriteRandomizer>();
        foreach (SpriteRandomizer spriteRandomizer in spriteRandomizers)
            spriteRandomizer.RandomizeSprite();

        foreach (GameObject toggleableElement in _toggleableElements)
            toggleableElement.SetActive(Random.value > 0.5f);
    }
}
