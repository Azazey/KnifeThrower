using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStorage : MonoBehaviour
{
    public static LevelStorage Storage { get; private set; }

    public List<LevelSettings> Levels = new List<LevelSettings>();

    private const string _currentLevel = "currentLevel";

    public List<LevelSettings> GetLevelList()
    {
        if (Levels.Count == 0)
        {
            CreateLevelList();
        }

        return Levels;
    }

    public LevelSettings GetCurrentLevel()
    {
        if (!PlayerPrefs.HasKey(_currentLevel))
        {
            SetStartLevel();
        }
        return Levels.Find(item => item.name == PlayerPrefs.GetString(_currentLevel));;
    }
    
    public void SetStartLevel()
    {
        PlayerPrefs.SetString(_currentLevel, Levels[0].name);
        PlayerPrefs.Save();
    }

    public void SetCurrentLevel(int id)
    {
        PlayerPrefs.SetString(_currentLevel, Levels[id].name);
        PlayerPrefs.Save();
    }

    private void CreateLevelList()
    {
        foreach (LevelSettings level in Resources.LoadAll<LevelSettings>("Levels/StandartGame")) 
        {
            Levels.Add(level);
            // Debug.Log("Objects was found: " + level.name);
        }
    }
    

    private void Awake()
    {
        if (Storage == null)
        {
            // Debug.Log("Storage was made");
            Storage = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            // Debug.Log("Founded another storage. Deleting unnecessary storage");
            Destroy(gameObject);
            return;
        }
        
        GetLevelList();
        SetStartLevel();
        // Debug.Log(GetCurrentLevel().name);
    }
}