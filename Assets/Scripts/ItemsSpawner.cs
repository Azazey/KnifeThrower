using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _knifePrefab;
    [SerializeField] private GameObject _applePrefab;
    [SerializeField]private int _appleSpawnChance;
    [SerializeField]private int _knifeSpawnChance;
    [SerializeField]private int _maxKnifes;
    [SerializeField]private int _maxApples;
    
    [SerializeField] private List<ItemHolder> _spawners = new List<ItemHolder>();

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
            _spawners.Add(transform.GetChild(i).GetComponent<ItemHolder>());
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
                    SpawnApples();
                }   
            }
        }
    }

    private void SpawnApples()
    {
        int itemHolster = Random.Range(0, _spawners.Count);
        if (_spawners[itemHolster].Item == Items.Nothing)
        {
            GameObject apple = Instantiate(_applePrefab, _spawners[itemHolster].transform.position, Quaternion.identity);
            apple.transform.rotation = _spawners[itemHolster].transform.rotation;
            apple.transform.parent = transform.parent;
            _spawners[itemHolster].Item = Items.Apple;
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
                    SpawnKnife();
                }   
            }
        }
    }

    private void SpawnKnife()
    {
        int itemHolster = Random.Range(0, _spawners.Count);
        if (_spawners[itemHolster].Item == Items.Nothing)
        {
            GameObject knife = Instantiate(_knifePrefab, _spawners[itemHolster].transform.position, _spawners[itemHolster].transform.rotation);
            knife.transform.parent = transform.parent;
            _spawners[itemHolster].Item = Items.Knife;
        }
    }
}
