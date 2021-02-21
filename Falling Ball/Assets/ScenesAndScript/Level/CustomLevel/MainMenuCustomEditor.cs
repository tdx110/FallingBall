using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuCustomEditor : MonoBehaviour
{
    [SerializeField]
    private LevelSetting levelSetting;

    private void Start()
    {
        GlobalVariable.ActualLvlVariable = GlobalVariable.lvlVariablesData[GlobalVariable.IndexLevel];
        levelSetting.gameObjectNextLevel.SetActive(GlobalVariable.ActualLvlVariable.GetShowNextLevelButton);
        levelSetting.gameObjectCoinText.GetComponent<Text>().text =
            PlayerPrefs.GetInt(GlobalVariable.GoldPlayerPrefs, 0).ToString();
        PlayerPrefs.SetInt(GlobalVariable.ActualLvlVariable.GetActualScene, 1);
        ShowPlay();
    }
    public void ShowPause()
    {
        if (!levelSetting.gameObjectGameOverCanvas.activeSelf && !levelSetting.gameObjectLevelCompleteCanvas.activeSelf)
        {
            Time.timeScale = 0;
            levelSetting.gameObjectPlayerUiCanvas.SetActive(true);
            levelSetting.gameObjectPauseCanvas.SetActive(true);
            levelSetting.gameObjectLevelCompleteCanvas.SetActive(false);
            levelSetting.gameObjectGameOverCanvas.SetActive(false);
            levelSetting.gameObjectControlCanvas.SetActive(false);
        }
    }
    public void ShowPlay()
    {
        levelSetting.gameObjectUpLimit.GetComponent<Reklama>().IncCount();
        Time.timeScale = 1;
        levelSetting.gameObjectPlayerUiCanvas.SetActive(true);
        levelSetting.gameObjectPauseCanvas.SetActive(false);
        levelSetting.gameObjectLevelCompleteCanvas.SetActive(false);
        levelSetting.gameObjectGameOverCanvas.SetActive(false);
        levelSetting.gameObjectControlCanvas.SetActive(true);
    }
    public void ShowLevelComplete()
    {
        if (!levelSetting.gameObjectGameOverCanvas.activeSelf)
        {
            PlayerPrefs.SetInt(GlobalVariable.ActualLvlVariable.GetNextLevelSceneName, 1);
            levelSetting.gameObjectPlayerUiCanvas.SetActive(true);
            levelSetting.gameObjectPauseCanvas.SetActive(false);
            levelSetting.gameObjectLevelCompleteCanvas.SetActive(true);
            levelSetting.gameObjectGameOverCanvas.SetActive(false);
            levelSetting.gameObjectControlCanvas.SetActive(false);
        }
    }
    public void ShowGameOver()
    {
        Time.timeScale = 0;
        if (!levelSetting.gameObjectLevelCompleteCanvas.activeSelf)
        {
            levelSetting.gameObjectPlayerUiCanvas.SetActive(true);
            levelSetting.gameObjectPauseCanvas.SetActive(false);
            levelSetting.gameObjectLevelCompleteCanvas.SetActive(false);
            levelSetting.gameObjectGameOverCanvas.SetActive(true);
            levelSetting.gameObjectControlCanvas.SetActive(false);
        }
    }
    public void RestartButton()
    {
        SceneManager.LoadSceneAsync(GlobalVariable.ActualLvlVariable.GetActualScene);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void NextLevelButton()
    {
        GlobalVariable.IndexLevel += 1;
        SceneManager.LoadSceneAsync(GlobalVariable.ActualLvlVariable.GetNextLevelSceneName);
    }
}
