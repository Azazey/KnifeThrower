using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinChanger : MonoBehaviour
{
    [SerializeField] private KnifeSpawner _knifeSpawner;
    [SerializeField] private SkinButton _buttonPrefab;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _spawnPosition;
    [SerializeField] private PlayerBelongsWriter _playerBelongsWriter;
    [SerializeField] private TextMeshProUGUI _knifeName;
    [SerializeField] private TextMeshProUGUI _knifeDesc;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private RectTransform _content;

    private Knife _currentKnife;
    private KnifeProperties _currentKnifeToChange;

    private List<GameObject> _buttons = new List<GameObject>();
    
    private void Start()
    {
        CreateSkinChangeButtons();
        _currentKnife = _knifeSpawner.SpawnCurrentKnife();
        _button.interactable = false;
        _buttonText.text = "Selected";
        _buttonText.alpha = 0.5f;
        UpdateKnifeInfo(KnifeStorage.Storage.GetCurrentKnife());
    }

    private void CreateSkinChangeButtons()
    {
        foreach (KnifeProperties knifePropertiese in KnifeStorage.Storage.GetKnifeList())
        {
            SkinButton skinButton = Instantiate(_buttonPrefab, _spawnPosition.transform);
            skinButton.Init(knifePropertiese);
            skinButton.SkinChanged += ReplaceKnife;
            _buttons.Add(skinButton.gameObject);
        }
    }

    private void ReplaceKnife(KnifeProperties knifeProperties)
    {
        Destroy(_currentKnife.gameObject);
        _currentKnife = _knifeSpawner.SpawnKnifeInSkinMenu(knifeProperties);
        _currentKnifeToChange = knifeProperties;
        UpdateKnifeInfo(_currentKnifeToChange);
        if (!knifeProperties.Unlocked && knifeProperties.Boughtable)
        {
            _buttonText.text = "Buy";
            _buttonText.alpha = 1;
            _button.interactable = true;
        }
        else if (!knifeProperties.Boughtable && !knifeProperties.Unlocked)
        {
            _buttonText.text = "Locked";
            _buttonText.alpha = 0.5f;
            _button.interactable = false;
        }
        else
        {
            if (knifeProperties == KnifeStorage.Storage.GetCurrentKnife())
            {
                _button.interactable = false;
                _buttonText.text = "Selected";
                _buttonText.alpha = 0.5f;
            }
            else
            {
                _button.interactable = true;
                _buttonText.text = "Select";
                _buttonText.alpha = 1f;
            }
        }

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(SetKnifeProperties);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
    }

    public void SetKnifeProperties()
    {
        Debug.Log("Button pressed:" + _currentKnifeToChange.Name);
        if (_currentKnifeToChange.Boughtable)
        {
            if (_currentKnifeToChange.Unlocked && _currentKnifeToChange.Prefab != null)
            {
                KnifeStorage.Storage.SetCurrentKnife(_currentKnifeToChange);
                Debug.Log("KnifeChanged:" + _currentKnifeToChange.Name);
            }
            else if (_currentKnifeToChange.MoneyCost <= PlayerPrefs.GetInt(PlayerBelongs.Money))
            {
                BuyAKnife();
                _buttonText.text = "Select";
                _buttonText.alpha = 1f;
            }
        }
        else if (_currentKnifeToChange.Unlocked)
        {
            KnifeStorage.Storage.SetCurrentKnife(_currentKnifeToChange);
            Debug.Log("KnifeChanged:" + _currentKnifeToChange.Name);
            _button.interactable = false;
        }

        if (_currentKnifeToChange != KnifeStorage.Storage.GetCurrentKnife()) return;
        _button.interactable = false;
        _buttonText.text = "Selected";
        _buttonText.alpha = 0.5f;
    }

    private void BuyAKnife()
    {
        _currentKnifeToChange.Unlocked = true;
        PlayerPrefs.SetInt(_currentKnifeToChange.name, Convert.ToInt32(_currentKnifeToChange.Unlocked));
        PlayerBelongs.OnKnifeBuy(_currentKnifeToChange.MoneyCost);
        _playerBelongsWriter.WriteMoney();
    }

    private void UpdateKnifeInfo(KnifeProperties knifeProperties)
    {
        _knifeName.text = knifeProperties.Name;
        if (knifeProperties.Boughtable && !knifeProperties.Unlocked)
        {
            _knifeDesc.text = knifeProperties.Description + "\r\n" + "Knife cost:" + knifeProperties.MoneyCost;
        }
        else if (knifeProperties.Unlocked)
        {
            _knifeDesc.text = knifeProperties.Description;
        }
        else
        {
            _knifeDesc.text = knifeProperties.Description + "\r\n" + "It can't be bought!";
        }
    }
}