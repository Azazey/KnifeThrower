using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinChanger : MonoBehaviour
{
    [SerializeField] private KnifeSpawner _knifeSpawner;
    [SerializeField] private SkinButton _buttonPrefab;

    private Knife _currentKnife;

    private void Start()
    {
        CreateSkinChangeButtons();
        _currentKnife = _knifeSpawner.SpawnCurrentKnife();
    }

    private void CreateSkinChangeButtons()
    {
        foreach (KnifeProperties knifePropertiese in KnifeStorage.Storage.GetKnifeList())
        {
            SkinButton skinButton = Instantiate(_buttonPrefab, transform);
            skinButton.Init(knifePropertiese);
            skinButton.SkinChanged += ReplaceKnife;
        }
    }

    private void ReplaceKnife()
    {
        Destroy(_currentKnife.gameObject);
        _currentKnife = _knifeSpawner.SpawnCurrentKnife();
    }

    public void SetKnifeProperties(Toggle change, int number)
    {
        Debug.Log("Button pressed:" + number);
        // Destroy(_currentKnife.gameObject);
        // _currentKnife = _knifeSpawner.SpawnKnife();
    }

}
