using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public void StartFirstLevel()
    {
        SceneManager.LoadScene("StartLevel");
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToSkinMenu()
    {
        SceneManager.LoadScene(1);
    }
}
