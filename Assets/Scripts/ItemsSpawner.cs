using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _knifePrefab;
    [SerializeField] private GameObject _applePrefab;
    [SerializeField] private int _appleSpawnChance;
    [SerializeField] private int _knifeSpawnChance;
    [SerializeField] private int _maxKnifes;
    [SerializeField] private int _maxApples;

    [SerializeField] private List<GameObject> _spawners = new List<GameObject>();

    private void Start()
    {
        InnitAllSpawns();
        TryToSpawnKnifes();
        TryToSpawnApples();
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
        item.transform.parent = transform.parent;
        _spawners.Remove(_spawners[itemHolster]);
    }
}