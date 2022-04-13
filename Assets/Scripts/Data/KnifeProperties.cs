using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Knife", menuName = "My Custom Stuff/Knife Creator/Make new Knife")]
public class KnifeProperties : ScriptableObject
{
    public string Name;
    public string Description;
    public GameObject Prefab;
    public Sprite Sprite;
}
