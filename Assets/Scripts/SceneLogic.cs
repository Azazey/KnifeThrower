using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLogic : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] _componentsToDisable;
    [SerializeField] private GameObject _looseWindow;
    [SerializeField] private GameObject _passWindow;
    [SerializeField] private int _knifeNeedToPassLevel = 5;
    [SerializeField] private KnifeSpawner _knifeSpawner;
    [SerializeField] private Log _log;
    
    private Knife _currentKnife;
    private int _currentKnifeCount;
    private bool _levelPassed;

    public Knife CurrentKnife => _currentKnife;

    public int KnifeNeedToPassLevel => _knifeNeedToPassLevel;

    public event Action OnKnifePinnedDown;

    public void SpawnKnife()
    {
        _currentKnife = _knifeSpawner.SpawnKnife();
    }

    public void ActivateLooseMenu()
    {
        _looseWindow.SetActive(true);
        for (int i = 0; i < _componentsToDisable.Length; i++)
        {
            _componentsToDisable[i].enabled = false;
        }
    }

    public void ActivatePassMenu()
    {
        _log.LogShatter();
        _passWindow.SetActive(true);
        for (int i = 0; i < _componentsToDisable.Length; i++)
        {
            _componentsToDisable[i].enabled = false;
        }
    }

    private void CheckPassedLevel()
    {
        if (_currentKnifeCount == _knifeNeedToPassLevel && !_levelPassed)
        {
            ActivatePassMenu();
            _levelPassed = true;
        }
    }

    private void CheckLoosing()
    {
        if (_currentKnife != null)
        {
            if (_currentKnife.CollidedWithKnife && !_levelPassed)
            {
                ActivateLooseMenu();
            }
        }
    }

    private void TryToSpawnKnife()
    {
        if (_currentKnife != null)
        {
            if (_currentKnife.IsPinnedDown)
            {
                _currentKnifeCount++;
                OnKnifePinnedDown.Invoke();
                if (_currentKnifeCount < _knifeNeedToPassLevel)
                {
                    SpawnKnife(); 
                }
            }
        }
    }

    private void Awake()
    {
        _looseWindow.SetActive(false);
        _passWindow.SetActive(false);
        SpawnKnife();
        _levelPassed = false;
        PlayerBelongs.OnGameStart();
    }

    private void Update()
    {
        TryToSpawnKnife();
        CheckPassedLevel();
        CheckLoosing();
    }
}