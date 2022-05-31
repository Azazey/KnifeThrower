using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class StageUI : MonoBehaviour
{
    [SerializeField] private Text _stageText;
    [SerializeField] private SceneLogic _sceneLogic;
    [SerializeField] private GameObject _stageBar;
    [SerializeField] private GameObject _stageStandartPrefab;
    [SerializeField] private GameObject _stageBossPrefab;
    [SerializeField] private float _timeToStageMove;

    [SerializeField] private int _progress;

    private List<GameObject> _stageBarParts = new List<GameObject>();
    private bool _bossAppear = false;

    private const string _progressBar = "progressBar";
    private const string _currentProgress = "currentProgress";

    private void WriteStage()
    {
        if (_stageText)
        {
            _stageText.text = "Stage " + PlayerPrefs.GetInt(_sceneLogic.LevelCount);
        }
    }

    private void DrawStageProgress()
    {
        if (LevelStorage.Storage.GetCurrentLevel().LevelType == LevelType.StartOfTier)
        {
            List<LevelSettings> levelList = LevelStorage.Storage.GetLevelList();
            foreach (var level in levelList)
            {
                _progress++;
                if (level.LevelType == LevelType.EndOfTier)
                {
                    break;
                }
            }

            PlayerPrefs.SetInt(_progressBar, _progress);
            PlayerPrefs.SetInt(_currentProgress, 1);
        }

        for (int i = 1; i < PlayerPrefs.GetInt(_progressBar); i++)
        {
            GameObject stagePart = Instantiate(_stageStandartPrefab, _stageBar.transform);
            _stageBarParts.Add(stagePart);
        }

        GameObject stageBoss = Instantiate(_stageBossPrefab, _stageBar.transform);
        _stageBarParts.Add(stageBoss);
        ChangeStageBarColour(PlayerPrefs.GetInt(_currentProgress));
    }

    private void ChangeStageBarColour(int count)
    {
        if (count < PlayerPrefs.GetInt(_progressBar))
        {
            for (int i = 0; i < count; i++)
            {
                _stageBarParts[i].GetComponent<Image>().color = Color.green;
            }
        }
        else if (count == PlayerPrefs.GetInt(_progressBar))
        {
            Vector3 bossPosition = new Vector3(0f, 1168.8f, 0f);
            GameObject boss = _stageBarParts[PlayerPrefs.GetInt(_currentProgress) - 1];
            boss.GetComponent<Image>().color = Color.red;
            boss.transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
            if (_bossAppear)
            {
                StartCoroutine(OnBossFight(bossPosition));   
            }
            else
            {
                _stageBar.transform.localPosition = bossPosition;
                _stageBar.GetComponent<HorizontalLayoutGroup>().spacing = -100;
            }
        }
        else
        {
            Vector3 originalPos = new Vector3(-270f, 1168.8f, 0f);
            ChangeStageBarColour(1);
            GameObject boss = _stageBarParts[PlayerPrefs.GetInt(_currentProgress) - 2];
            boss.GetComponent<Image>().color = Color.white;
            boss.transform.GetChild(0).GetComponent<Image>().color = Color.white;
            StartCoroutine(AfterBossFight(originalPos));
        }
    }

    private void OnLevelPass()
    {
        int currentProgress = PlayerPrefs.GetInt(_currentProgress);
        currentProgress++;
        PlayerPrefs.SetInt(_currentProgress, currentProgress);
        if (currentProgress == PlayerPrefs.GetInt(_progressBar))
        {
            _bossAppear = true;
        }
        ChangeStageBarColour(PlayerPrefs.GetInt(_currentProgress));
        WriteStage();
    }

    private IEnumerator PopUp()
    {
        yield return null;
    }
    
    private IEnumerator OnBossFight(Vector3 bossPosition)
    {
        for (float t = 0; t < _timeToStageMove; t += Time.deltaTime)
        {
            _stageBar.transform.localPosition =
                Vector3.Lerp(_stageBar.transform.localPosition, bossPosition, t / _timeToStageMove);
            _stageBar.GetComponent<HorizontalLayoutGroup>().spacing = Mathf.Lerp(0, -100, t / _timeToStageMove);
            yield return null;
        }
    }

    private IEnumerator AfterBossFight(Vector3 originalPosition)
    {
        for (float t = 0; t < _timeToStageMove; t += Time.deltaTime)
        {
            _stageBar.transform.localPosition =
                Vector3.Lerp(_stageBar.transform.localPosition, originalPosition, t / _timeToStageMove);
            _stageBar.GetComponent<HorizontalLayoutGroup>().spacing = 0;
            yield return null;
        }
    }

    private void Start()
    {
        _sceneLogic.OnLevelPass += OnLevelPass;
        _progress = 0;
        DrawStageProgress();
        WriteStage();
    }
}