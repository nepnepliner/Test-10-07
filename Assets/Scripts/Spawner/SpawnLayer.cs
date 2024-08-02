using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnLayer
{
    [SerializeField] private string _name;
    [SerializeField] private GameObject[] _spawnVariants;
    [SerializeField] private float _spawnsSize;

    [Header("Spawn On Start")]
    [SerializeField] private bool _doSpawnOnStart;
    [SerializeField] private int _spawnOnStartAmount;

    public string Name => _name;

    public GameObject RandomSpawnVariant
    {
        get
        {
            if (_spawnVariants != null && _spawnVariants.Length > 0)
                return _spawnVariants[Random.Range(0, _spawnVariants.Length)];
            return null;
        }
    }

    public float SpawnsSize => _spawnsSize;

    public bool DoSpawnOnStart => _doSpawnOnStart;

    public int SpawnOnStartAmount => _spawnOnStartAmount;
}
