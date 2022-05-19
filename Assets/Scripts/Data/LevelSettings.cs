using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "My Custom Stuff/Level Creator/Create new Level Settings")]
public class LevelSettings : ScriptableObject
{
    public int MaxAppleOnLevel;
    public int MaxKnifeOnLevel;
    public int SpawnKnifeChance;
    public int SpawnAppleChance;
    public AnimationCurve LogRotation;
}
