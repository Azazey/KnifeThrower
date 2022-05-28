using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] private Text _stageText;
    [SerializeField] private SceneLogic _sceneLogic;

    private void WriteStage()
    {
        if (_stageText)
        {
            _stageText.text = "Stage " + PlayerPrefs.GetInt(_sceneLogic.LevelCount);
        }
    }

    private void DrawStageProgress()
    {
        if (LevelStorage.Storage.GetCurrentLevel() == LevelStorage.Storage.Levels[0])
        {
            
        }
    }

    private void Start()
    {
        WriteStage();
    }
}