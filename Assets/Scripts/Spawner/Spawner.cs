using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    [SerializeField] private SpawnLayer[] _spawnLayers;

    [Header("Mask")]
    [SerializeField] private LayerMask _spawnMask;

    private void Start()
    {
        SpawnOnStart();
    }

    private void SpawnOnStart()
    {
        foreach (SpawnLayer spawnLayer in _spawnLayers)
        {
            if (spawnLayer == null || spawnLayer.DoSpawnOnStart == false)
                continue;
            for (int i = 0; i < spawnLayer.SpawnOnStartAmount; i++)
                Spawn(spawnLayer.RandomSpawnVariant, spawnLayer.SpawnsSize);
        }
    }

    public void Spawn(GameObject prefab, float spawnSize, int tryCount = 100)
    {
        if (prefab == null)
            return;

        Location location = Location.Instance;
        for (int i = 0; i < tryCount; i++)
        {
            Vector2 position = location.RandomPosition(location.PossibleArea(spawnSize));
            //Debug.Log("Try " + i + " : " + position);
            if (Physics2D.OverlapCircle(position, spawnSize, _spawnMask) == false)
            {
                GameObject spawn = Instantiate(prefab, position, Quaternion.identity);
                Debug.Log(spawn + " Spawned at " + position);
                break;
            }
        }
    }
}
