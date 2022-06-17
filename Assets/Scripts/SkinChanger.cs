using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinChanger : MonoBehaviour
{
    [SerializeField] private KnifeSpawner _knifeSpawner;
    [SerializeField] private SkinButton _buttonPrefab;
    [SerializeField] private GameObject _Button;

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

    private void ReplaceKnife(KnifeProperties knifeProperties)
    {
        Destroy(_currentKnife.gameObject);
        _currentKnife = _knifeSpawner.SpawnKnifeInSkinMenu(knifeProperties);
    }

    public void SetKnifeProperties(KnifeProperties knifeProperties)
    {
        Debug.Log("Button pressed:");
        // Destroy(_currentKnife.gameObject);
        // _currentKnife = _knifeSpawner.SpawnKnife();
    }

}
