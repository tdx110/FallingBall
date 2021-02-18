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
        Time.timeScale = 1;
        //ShowPlay();
        //if (GlobalVariable.ActualLvlVariable != null)
        //    levelSetting.gameObjectNextLevel.SetActive(GlobalVariable.ActualLvlVariable.GetShowNextLevelButton);
        //PlayerPrefs.SetInt(GlobalVariable.ActualLvlVariable.GetActualScene,1);
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
            levelSetting.gameObjectPlayerUiCanvas.SetActive(true);
            levelSetting.gameObjectPauseCanvas.SetActive(false);
            levelSetting.gameObjectLevelCompleteCanvas.SetActive(true);
            levelSetting.gameObjectGameOverCanvas.SetActive(false);
            levelSetting.gameObjectControlCanvas.SetActive(false);
        }
    }
    public void ShowGameOver()
    {
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
        SceneManager.LoadSceneAsync(GlobalVariable.ActualLvlVariable.GetNextLevelSceneName);
        PlayerPrefs.SetInt(GlobalVariable.ActualLvlVariable.GetNextLevelSceneName, 1);
    }
}
