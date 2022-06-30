using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossKnifeReward : MonoBehaviour
{
    [SerializeField] private float _lerp;
    [SerializeField] private GameObject _knifeIcon;
    [SerializeField] private SceneLogic _sceneLogic;
    [SerializeField] private Image _knifeImage;
    [SerializeField] private TextMeshProUGUI _knifeName;

    private IEnumerator PopUp()
    {
        for (float t = 0; t < _lerp; t += Time.deltaTime)
        {
            _knifeIcon.transform.localScale =
                Vector3.Lerp(_knifeIcon.transform.localScale, new Vector3(1f, 1f, 1f), t / _lerp);
            yield return null;
        }
    }

    private void OnBossFightWon()
    {
        _knifeIcon.SetActive(true);
        _knifeIcon.transform.localScale = Vector3.zero;
        StartCoroutine(PopUp());
    }

    private void Start()
    {
        if (LevelStorage.Storage.GetCurrentLevel().OpenKnife &&
            !Convert.ToBoolean(PlayerPrefs.GetInt(LevelStorage.Storage.GetCurrentLevel().OpenKnife.name)))
        {
            if (LevelStorage.Storage.GetCurrentLevel().OpenKnife.Sprite != null)
            {
                _knifeImage.sprite = LevelStorage.Storage.GetCurrentLevel().OpenKnife.Sprite;
            }

            if (LevelStorage.Storage.GetCurrentLevel().OpenKnife.Name != null)
            {
                _knifeName.text = LevelStorage.Storage.GetCurrentLevel().OpenKnife.Name;
            }

            _sceneLogic.OnLevelPass += OnBossFightWon;
        }

        _knifeIcon.SetActive(false);
    }
}