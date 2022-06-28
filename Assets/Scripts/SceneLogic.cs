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
    [SerializeField] private GameObject[] _objectsToHide;
    [SerializeField] private AudioSource _fallSound;
    [SerializeField] private GameObject _looseWindow;
    [SerializeField] private GameObject _passWindow;
    [SerializeField] private KnifeSpawner _knifeSpawner;
    [SerializeField] private Log _log;
    [SerializeField] private float _looseMenuTimeDelay;
    [SerializeField] private float _nextLevelTimeDelay;

    private Knife _currentKnife;
    private int _currentKnifeCount;
    private int _levelPassedInRow;
    private int _knifeNeedToPassLevel;
    private bool _levelPassed;
    private bool _levelFailed;

    private const string _levelCount = "levelCount";
    
    public string LevelCount => _levelCount;

    public Knife CurrentKnife => _currentKnife;

    public int KnifeNeedToPassLevel => _knifeNeedToPassLevel;

    public event Action OnKnifePinnedDown;
    public event Action OnLevelPass;

    public void SpawnKnife()
    {
        _currentKnife = _knifeSpawner.SpawnCurrentKnife();
    }

    public void ActivateLooseMenu()
    {
        _fallSound.Play();
        for (int i = 0; i < _componentsToDisable.Length; i++)
        {
            _componentsToDisable[i].enabled = false;
        }

        foreach (GameObject oneObject in _objectsToHide)
        {
            oneObject.SetActive(false);
        }
        
        StartCoroutine(MenuDelay(_looseWindow, _looseMenuTimeDelay));
    }

    public void ActivatePassMenu()
    {
        _log.LogShatter();
        for (int i = 0; i < _componentsToDisable.Length; i++)
        {
            _componentsToDisable[i].enabled = false;
        }

        _levelPassedInRow++;
        PlayerPrefs.SetInt(_levelCount, _levelPassedInRow);
        OnLevelPass?.Invoke();
        StartCoroutine(NextLevel( _nextLevelTimeDelay));
    }

    private IEnumerator NextLevel(float timeDelay)
    {
        WaitForSeconds delay = new WaitForSeconds(timeDelay);
        yield return delay;
        SceneManager.LoadScene(2);
        if (LevelStorage.Storage.GetCurrentLevel() !=
            LevelStorage.Storage.GetLevelList()[LevelStorage.Storage.GetLevelList().Count - 1])
        {
            LevelStorage.Storage.SetCurrentLevel(LevelStorage.Storage.GetLevelList()
                .FindIndex(item => item == LevelStorage.Storage.GetCurrentLevel()) + 1);
        }
        else
        {
            LevelStorage.Storage.SetCurrentLevel(LevelStorage.Storage.GetLevelList().Count - 6);
        }
    }

    private IEnumerator MenuDelay(GameObject prefab, float timeDelay)
    {
        WaitForSeconds delay = new WaitForSeconds(timeDelay);
        yield return delay;
        prefab.SetActive(true);
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
                if (!_levelFailed)
                {
                    ActivateLooseMenu();
                    _levelFailed = true;
                }
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
        _knifeNeedToPassLevel = LevelStorage.Storage.GetCurrentLevel().KnifeNeedToPassLevel;
        SpawnKnife();
        _levelPassed = false;
        Debug.Log(LevelStorage.Storage.GetCurrentLevel().name);
        if (LevelStorage.Storage.GetCurrentLevel() == LevelStorage.Storage.Levels[0])
        {
            PlayerBelongs.OnGameStart();
            _levelPassedInRow = 1;
            PlayerPrefs.SetInt(_levelCount, _levelPassedInRow);
        }

        _levelPassedInRow = PlayerPrefs.GetInt(_levelCount);
        _levelFailed = false;
    }

    private void Update()
    {
        TryToSpawnKnife();
        CheckPassedLevel();
        CheckLoosing();
    }
}