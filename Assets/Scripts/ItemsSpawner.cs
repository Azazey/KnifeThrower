using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _knifePrefab;
    [SerializeField] private GameObject _applePrefab;
    [SerializeField] private KnifeProperties _knifeSettingsToSpawn;
    
    private int _appleSpawnChance;
    private int _knifeSpawnChance;
    private int _maxKnifes;
    private int _maxApples;

    [SerializeField] private List<GameObject> _spawners = new List<GameObject>();

    private void Start()
    {
        GetLevelSettings();
        InnitAllSpawns();
        TryToSpawnKnifes();
        TryToSpawnApples();
    }

    private void GetLevelSettings()
    {
        _appleSpawnChance = LevelStorage.Storage.GetCurrentLevel().SpawnAppleChance;
        _knifeSpawnChance = LevelStorage.Storage.GetCurrentLevel().SpawnKnifeChance;
        _maxKnifes = LevelStorage.Storage.GetCurrentLevel().MaxKnifeOnLevel;
        _maxApples = LevelStorage.Storage.GetCurrentLevel().MaxAppleOnLevel;
    }

    private void InnitAllSpawns()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _spawners.Add(transform.GetChild(i).gameObject);
        }
    }

    private void TryToSpawnApples()
    {
        if (_spawners != null)
        {
            for (int i = 0; i < _maxApples; i++)
            {
                if (Random.Range(0, 100) < _appleSpawnChance)
                {
                    SpawnObject(_applePrefab);
                }
            }
        }
    }

    private void TryToSpawnKnifes()
    {
        if (_spawners != null)
        {
            for (int i = 0; i < _maxKnifes; i++)
            {
                if (Random.Range(0, 100) < _knifeSpawnChance)
                {
                    SpawnObject(_knifePrefab);
                }
            }
        }
    }

    private void SpawnObject(GameObject prefab)
    {
        if (_spawners.Count == 0)
        {
            Debug.Log("Can't spawn object:" + prefab.name + "." + " Due of reached spawnpoint limit");
            return;
        }

        int itemHolster = Random.Range(0, _spawners.Count);
        GameObject item = Instantiate(prefab, _spawners[itemHolster].transform.position,
            _spawners[itemHolster].transform.rotation);
        if (item.GetComponent<Knife>())
        {
            item.GetComponent<Knife>()
                .Init(_knifeSettingsToSpawn);
            item.GetComponent<Knife>().IsPinnedDown = true;
        }

        item.transform.parent = transform.parent;
        _spawners.Remove(_spawners[itemHolster]);
    }
}