using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public void StartFirstLevel()
    {
        SceneManager.LoadScene(2);
        LevelStorage.Storage.SetStartLevel();
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToSkinMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToSettings()
    {
        SceneManager.LoadScene(3);
    }
}