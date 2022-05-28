using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeUI : MonoBehaviour
{
    [SerializeField] private SceneLogic _sceneLogic;
    [SerializeField] private GameObject _knifeImagePrefab;

    [SerializeField] private List<GameObject> _knifeImages = new List<GameObject>();

    private void SpawnKnifeImages()
    {
        for (int i = 0; i < _sceneLogic.KnifeNeedToPassLevel; i++)
        {
            GameObject newKnife = Instantiate(_knifeImagePrefab, transform);
            _knifeImages.Add(newKnife);
        }
    }

    private void DeleteImage()
    {
        int lastKnife = _knifeImages.Count - 1;
        if (_knifeImages.Count != 0)
        {
            Destroy(_knifeImages[lastKnife]);
            _knifeImages.Remove(_knifeImages[lastKnife]);
        }
    }

    private void Start()
    {
        _sceneLogic.OnKnifePinnedDown += DeleteImage;
        SpawnKnifeImages();
    }
}
