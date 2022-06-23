using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    [SerializeField] private Image _knifeIcon;
    [SerializeField] private Button _button;

    private KnifeProperties _knifeProperties;

    public KnifeProperties KnifeProperties => _knifeProperties;

    public event Action<KnifeProperties> SkinChanged;

    public void Init(KnifeProperties knifeProperties)
    {
        _knifeIcon.sprite = knifeProperties.Sprite;
        _knifeProperties = knifeProperties;
        _button.onClick.AddListener(ChangeSkin);
    }

    private void ChangeSkin()
    {
        // KnifeStorage.Storage.SetCurrentKnife(_knifeProperties);
        SkinChanged?.Invoke(_knifeProperties);
    }
}
