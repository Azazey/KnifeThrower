using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KnifePrefabData
{
    private static GameObject _knifePrefab;

    public static GameObject KnifePrefab
    {
        get => _knifePrefab;
        set => _knifePrefab = value;
    }
}
