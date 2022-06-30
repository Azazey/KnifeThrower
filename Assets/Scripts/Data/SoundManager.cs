using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Manager { get; private set; }
    
    [SerializeField] private AudioSource _menuMusic;
    
    public AudioSource MenuMusic => _menuMusic;

    public const string Volume = "Volume";

    public const string Music = "Music";



    private void Awake()
    {
        if (Manager == null)
        {
            // Debug.Log("Storage was made");
            Manager = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            // Debug.Log("Founded another storage. Deleting unnecessary storage");
            Destroy(gameObject);
            return;
        }
        CheckSettings();
        AudioListener.volume = PlayerPrefs.GetFloat(Volume);
        if (_menuMusic)
        {
            _menuMusic.enabled = Convert.ToBoolean(PlayerPrefs.GetInt(Music));
        }
    }

    private void CheckSettings()
    {
        if (!PlayerPrefs.HasKey(Volume) && !PlayerPrefs.HasKey(Music))
        {
            SetStandartSettings();
        }
    }

    private void SetStandartSettings()
    {
        PlayerPrefs.SetFloat(Volume, 1);
        PlayerPrefs.SetInt(Music, 1);
    }
}
