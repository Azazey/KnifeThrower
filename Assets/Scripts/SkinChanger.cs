using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    [SerializeField] private KnifeSpawner _knifeSpawner;
    
    private void Start()
    {
        _knifeSpawner.SpawnKnife();
    }
}
