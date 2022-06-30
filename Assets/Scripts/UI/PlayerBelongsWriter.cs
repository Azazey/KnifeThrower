using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBelongsWriter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _highScore;
    [SerializeField] private TextMeshProUGUI _money;
    [SerializeField] private SceneLogic _sceneLogic;

    public void WriteScore()
    {
        if (_score)
        {
            _score.text = "Score:" + PlayerPrefs.GetInt(PlayerBelongs.Score);
        }
    }

    public void WriteHighScore()
    {
        if (_highScore)
        {
            PlayerBelongs.SetHighScore();
            _highScore.text = "High Score:" + PlayerPrefs.GetInt(PlayerBelongs.HighScore);
        }
    }

    public void WriteMoney()
    {
        if (_money)
        {
            _money.text = "Money:" + PlayerPrefs.GetInt(PlayerBelongs.Money);
        }
    }

    private void Start()
    {
        if (_sceneLogic != null)
        {
            _sceneLogic.OnKnifePinnedDown += WriteScore;
            _sceneLogic.OnKnifePinnedDown += WriteMoney;   
        }
        WriteMoney();
        WriteScore();
        WriteHighScore();
    }
}
