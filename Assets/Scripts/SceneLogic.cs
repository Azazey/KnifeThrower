using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneLogic : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] _componentsToDisable;
    [SerializeField] private GameObject _looseWindow;
    [SerializeField] private KnifeSpawner _knifeSpawner;

    private Knife _currentKnife;

    public Knife CurrentKnife => _currentKnife;

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

    private void CheckLoosing()
    {
        if (_currentKnife != null)
        {
            if (_currentKnife.CollidedWithKnife)
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
                SpawnKnife();
            }
        }
    }

    private void Start()
    {
        SpawnKnife();
    }

    private void Update()
    {
        TryToSpawnKnife();
        CheckLoosing();
    }
}