using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiButtonLevelScript : MonoBehaviour
{
    #region Variable
    [Header("Przycisk")]
    public Button ButtonLevel;
    [Header("Pole tekstowe.")]
    public Text TextButton;
    [Header("Dane do danego poziomu.")]
    public LvlVariable LvlVariableData;
    [Header("Wybrany poziom do obs�ugi.")]
    public GlobalVariable.LvlList LvlSelect;
    #endregion
    #region Function
    public void SetButton()
    {
        LvlVariableData = GlobalVariable.lvlVariablesData[(int)LvlSelect];
        TextButton.text = LvlVariableData.GetTextButton();
        ButtonLevel.gameObject.name = "UiButtonLevel " + LvlVariableData.GetTextButton();
    }
    private void Awake()
    {
        SetButton();
    }
    #endregion

}
