using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiButtonLevelScript : MonoBehaviour
{
    #region Variable
    [Header("Przycisk")]
    public Button ButtonLevel;
    [Header("Pole tekstowe.")]
    public Text TextButton;
    [Header("Wybrany poziom do obs³ugi.")]
    public GlobalVariable.LvlList LvlSelect;
    #endregion
    #region Function
    public void SetButton()
    {
        TextButton.text = GlobalVariable.lvlVariablesData[(int)LvlSelect].GetTextButton;
        ButtonLevel.gameObject.name = "UiButtonLevel " + GlobalVariable.lvlVariablesData[(int)LvlSelect].GetTextButton;
    }
    private void Awake()
    {
        SetButton();
        if (PlayerPrefs.GetInt(GlobalVariable.lvlVariablesData[(int)LvlSelect].GetActualScene, 0) == 1) ButtonLevel.interactable = true;
        else ButtonLevel.interactable = false;
    }
    public void ClickButton()
    {
        GlobalVariable.IndexLevel = (int)LvlSelect;
        GlobalVariable.ActualLvlVariable = GlobalVariable.lvlVariablesData[(int)LvlSelect];
        SceneManager.LoadSceneAsync(GlobalVariable.lvlVariablesData[(int)LvlSelect].GetActualScene);
    }
    #endregion

}
