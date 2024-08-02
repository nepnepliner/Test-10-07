using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : Singleton<Location>
{
    [SerializeField] private Vector2 _min;
    [SerializeField] private Vector2 _max;

    [SerializeField] private float _borderWidht;

    public Vector2 Min => _min;
    public Vector2 Max => _max;
    public Rect Rect => MathRect(_min, _max);

    private void Start()
    {
        AddBorders();
    }

    private Rect MathRect(Vector2 min, Vector2 max) => MathRect(min.x, max.x, min.y, max.y);

    private Rect MathRect(float minX, float maxX, float minY, float maxY)
    {
        return new Rect(minX, minY, maxX - minX, maxY - minY);
    }

    private void AddBorders()
    {
        AddBorder(_min.x - _borderWidht, _min.x, _min.y - _borderWidht, _max.y + _borderWidht);
        AddBorder(_max.x, _max.x + _borderWidht, _min.y - _borderWidht, _max.y + _borderWidht);
        AddBorder(_min.x, _max.x, _min.y - _borderWidht, _min.y);
        AddBorder(_min.x, _max.x, _max.y, _max.y + _borderWidht);
    }

    private void AddBorder(float minX, float maxX, float minY, float maxY) => AddBorder(MathRect(minX, maxX, minY, maxY));

    private void AddBorder(Rect rect)
    {
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.offset = rect.center;
        collider.size = rect.size;
    }

    public Rect PossibleArea(float objectSize) => PossibleArea(new Vector2(objectSize, objectSize));

    public Rect PossibleArea(Vector2 objectSize)
    {
        Rect clampedRect = Rect;
        Vector2 halfSize = objectSize * 0.5f;
        clampedRect.xMin += halfSize.x;
        clampedRect.xMax -= halfSize.x;
        clampedRect.yMin += halfSize.y;
        clampedRect.yMax -= halfSize.y;
        return clampedRect;
    }

    public Vector3 ClampPosition(Vector3 position, Rect possibleArea)
    {
        Vector3 clampedPosition = new Vector3(Mathf.Clamp(position.x, possibleArea.xMin, possibleArea.xMax),
                                              Mathf.Clamp(position.y, possibleArea.yMin, possibleArea.yMax),
                                              position.z);
        return clampedPosition;
    }

    public Vector2 RandomPosition(Rect possibleArea)
    {
        return new Vector2(Random.Range(possibleArea.xMin, possibleArea.xMax),
                           Random.Range(possibleArea.yMin, possibleArea.yMax));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0.9f, 0.1f, 0.5f);
        Rect rect = Rect;
        Gizmos.DrawCube(rect.center, rect.size);
    }
}
