using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerBelongs
{
    public const string Money = "Money";
    public const string Score = "Score";
    public const string HighScore = "HighScore";
    private const int _resetValue = 0;

    public static void AddScore(int value)
    {
        int currentScore = PlayerPrefs.GetInt(Score);
        currentScore += value;
        PlayerPrefs.SetInt(Score, currentScore);
    }

    public static void AddMoney(int value)
    {
        int money = PlayerPrefs.GetInt(Money);
        money += value;
        PlayerPrefs.SetInt(Money, money);
    }

    public static void SetHighScore()
    {
        if (PlayerPrefs.GetInt(Score) > PlayerPrefs.GetInt(HighScore))
        {
            PlayerPrefs.SetInt(HighScore, PlayerPrefs.GetInt(Score));
        }
    }

    public static void ResetAllBelongs()
    {
        PlayerPrefs.SetInt(Score, _resetValue);
        PlayerPrefs.SetInt(Money, _resetValue);
        PlayerPrefs.SetInt(HighScore, _resetValue);
    }

    public static void ResetScore()
    {
        PlayerPrefs.SetInt(HighScore, _resetValue);
    }

    public static void ResetMoney()
    {
        PlayerPrefs.SetInt(Money, _resetValue);
    }

    public static void OnGameStart()
    {
        PlayerPrefs.SetInt(Score, _resetValue);
    }
}
