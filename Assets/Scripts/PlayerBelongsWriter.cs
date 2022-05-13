using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBelongsWriter : MonoBehaviour
{
    [SerializeField] private Text _score;
    [SerializeField] private Text _highScore;
    [SerializeField] private Text _money;

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

    private void Awake()
    {
        WriteMoney();
        WriteScore();
        WriteHighScore();
    }
}
