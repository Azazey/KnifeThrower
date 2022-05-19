using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Items
{
    Nothing,
    Knife,
    Apple,
    SomethingElse
}


public class ItemHolder : MonoBehaviour
{
    [SerializeField] private Items _item;

    public Items Item
    {
        get => _item;
        set => _item = value;
    }
}
