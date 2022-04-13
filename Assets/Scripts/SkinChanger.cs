using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinChanger : MonoBehaviour
{
    [SerializeField] private KnifeSpawner _knifeSpawner;
    [SerializeField] private Toggle _tooglePrefab;

    private Knife _currentKnife;

    private void Start()
    {
        CreateToggles();
        _currentKnife = _knifeSpawner.SpawnKnife();
    }

    private void CreateToggles()
    {
        for (int i = 0; i < KnifeStorage.Storage.GetKnifeList().Count; i++)
        {
            Toggle toggle = Instantiate(_tooglePrefab, transform);
            toggle.onValueChanged.AddListener(delegate {
                SetKnifeProperties(toggle, i);
            });

        }
    }

    public void SetKnifeProperties(Toggle change, int number)
    {
        Debug.Log("Button pressed:" + number);
        // Destroy(_currentKnife.gameObject);
        // _currentKnife = _knifeSpawner.SpawnKnife();
    }

}
