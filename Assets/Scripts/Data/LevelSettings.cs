using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelType{
    StartOfTier,
    Between,
    EndOfTier
}

[CreateAssetMenu(fileName = "New Level", menuName = "My Custom Stuff/Level Creator/Create new Level Settings")]
public class LevelSettings : ScriptableObject
{
    public int MaxAppleOnLevel;
    public int MaxKnifeOnLevel;
    public int SpawnKnifeChance;
    public int SpawnAppleChance;
    public int KnifeNeedToPassLevel;
    public Material LogMaterial;
    public AudioClip BreakSound;
    public KnifeProperties OpenKnife;
    public AnimationCurve LogRotation;
    public LevelType LevelType;
}
