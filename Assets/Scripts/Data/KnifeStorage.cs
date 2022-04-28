using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeStorage : MonoBehaviour
{
    public static KnifeStorage Storage { get; private set; }
    
    public List<KnifeProperties> Knifes = new List<KnifeProperties>();

    private const string currentKnife = "currentKnife";

    public List<KnifeProperties> GetKnifeList()
    {
        if (Knifes.Count == 0)
        {
            CreateKnifeList();
        }

        return Knifes;
    }

    public KnifeProperties GetCurrentKnife()
    {
        if (!PlayerPrefs.HasKey(currentKnife))
        {
            SetStandartKnife();
        }
        return Knifes.Find(item => item.Name == PlayerPrefs.GetString("currentKnife"));
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
        GetKnifeList();
        GetCurrentKnife();
    }

    private void SetStandartKnife()
    {
        PlayerPrefs.SetString(currentKnife, Knifes[0].Name);
        PlayerPrefs.Save();
    }

    private void CreateKnifeList()
    {
        foreach (KnifeProperties knife in Resources.LoadAll<KnifeProperties>("Knifes")) 
        {
            Knifes.Add(knife);
            // Debug.Log("Objects was found: " + knife.name);
        }
    }
}