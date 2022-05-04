using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    [SerializeField] private int _applesPerLevel;
    [SerializeField] private int _knifesPerLevel;
    [SerializeField] private GameObject _knifePrefab;
    [SerializeField] private GameObject _applePrefab;
    private GameObject[] _spawners;

    private void Start()
    {
        
    }

    private void TryToSpawnObjects()
    {
        if (_spawners != null)
        {
            
        }
    }
}
