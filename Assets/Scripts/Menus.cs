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

    // public void NextLevel()
    // {
    //     SceneManager.LoadScene(2);
    //     if (LevelStorage.Storage.GetCurrentLevel() !=
    //         LevelStorage.Storage.GetLevelList()[LevelStorage.Storage.GetLevelList().Count - 1])
    //     {
    //         LevelStorage.Storage.SetCurrentLevel(LevelStorage.Storage.GetLevelList()
    //             .FindIndex(item => item == LevelStorage.Storage.GetCurrentLevel()) + 1);
    //     }
    //     else
    //     {
    //         LevelStorage.Storage.SetCurrentLevel(LevelStorage.Storage.GetLevelList().Count - 6);
    //     }
    // }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToSkinMenu()
    {
        SceneManager.LoadScene(1);
    }
}