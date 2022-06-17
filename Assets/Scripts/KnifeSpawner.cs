using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    [SerializeField] private Knife _knifePrefab;

    public Knife SpawnCurrentKnife()
    {
        Knife knife = Instantiate(_knifePrefab, transform.position, transform.rotation);
        knife.Init(KnifeStorage.Storage.GetCurrentKnife());
        return knife;
    }

    public Knife SpawnKnifeInSkinMenu(KnifeProperties knifeProperties)
    {
        Knife knife = Instantiate(_knifePrefab, transform.position, transform.rotation);
        knife.Init(knifeProperties);
        return knife;
    }
}