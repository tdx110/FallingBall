using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GlobalVariable
{
    #region Variable
    /// <summary>
    /// Zapisane dane o bie¿¹cym poziomie
    /// </summary>
    public static LvlVariable ActualLvlVariable;
    public static int ActualGold;
    public static int AdcCount = 4;

    #endregion

    #region Miejsce zapisów w PlayerPrefs
    public static string GoldPlayerPrefs = "Gold";
    public static string AdsViverPlayerPrefs = "Ads";
    #endregion
    #region Dane Poziomów
    public static LvlVariable[] lvlVariablesData = new LvlVariable[]
        {
          new LvlVariable("Lvl2","Lvl1","Lv 1","1", false),
          new LvlVariable("Lvl3","Lvl2","Lv 2","2", false),
          new LvlVariable("Lvl4","Lvl3","Lv 3","3", false),
          new LvlVariable("Lvl5","Lvl4","Lv 4","4", false),
          new LvlVariable("Lvl6","Lvl5","Lv 5","5", false),
          new LvlVariable("Lvl7","Lvl6","Lv 6","6", false),
          new LvlVariable("Lvl8","Lvl7","Lv 7","7", false),
          new LvlVariable("Lvl9","Lvl8","Lv 8","8", false),
          new LvlVariable("Lvl10","Lvl9","Lv 9","9", false),
          new LvlVariable("Lvl11","Lvl10","Lv 10","10", false),
          new LvlVariable("Lvl12","Lvl11","Lv 11","11", false),
        };

    #endregion
    #region Enum-y
    public enum LvlList
    {
        Lvl1 = 0,
        Lvl2 = 1,
        Lvl3 = 2,
        Lvl4 = 3,
        Lvl5 = 4,
        Lvl6 = 5,
        Lvl7 = 6,
        Lvl8 = 7,
        Lvl9 = 8,
        Lvl10 = 9,
        Lvl11 = 10,
    }
    #endregion
}
/// <summary>
/// Funkcja tworz¹ca wszystkie dane potrzebne do poziomu
/// </summary>
public class LvlVariable
{
    #region Zmienne
    /// <summary>
    /// Nazwa nastêpnej sceny do aktywacji w PlayerPrefs
    /// PlayerPrefs.SetInt(nextLevelPlayerPrefs, 1);
    /// </summary>
    private string nextLevelSceneName;
    /// <summary>
    /// Nazwa sceny do wczytania w LoadScene
    /// </summary>
    private string actualScene;
    /// <summary>
    /// Nazwa aktualnej sceny do wyœwietlenia po przejœciu
    /// </summary>
    private string sceneNameComplete;
    /// <summary>
    /// Czy ma pokazaæ przejœcie na nastêpny poziom
    /// </summary>
    private bool showNextLevelButton;
    /// <summary>
    /// Napis wyœwietlany na przycisku w "New Game"
    /// </summary>
    private string textButton;
    /// <summary>
    /// wielkoœæ wyœwietlanego tekstu
    /// </summary>
    private int fontSize;
    #endregion

    #region Konstruktory
    public LvlVariable(string _actualScene, string _sceneNameComplete)
    {
        nextLevelSceneName = "";
        actualScene = _actualScene;
        sceneNameComplete = _sceneNameComplete;
        showNextLevelButton = false;
        fontSize = 100;
    }
    public LvlVariable(string _nextLevelSceneName, string _actualScene, string _sceneNameComplete)
    {
        nextLevelSceneName = _nextLevelSceneName;
        actualScene = _actualScene;
        sceneNameComplete = _sceneNameComplete;
        showNextLevelButton = true;
        fontSize = 100;
    }
    public LvlVariable(string _nextLevelSceneName, string _actualScene, string _sceneNameComplete, bool _showNextLevelButton)
    {
        nextLevelSceneName = _nextLevelSceneName;
        actualScene = _actualScene;
        sceneNameComplete = _sceneNameComplete;
        showNextLevelButton = _showNextLevelButton;
        fontSize = 100;
    }
    public LvlVariable(string _nextLevelSceneName, string _actualScene, string _sceneNameComplete,
        string _textButton, bool _showNextLevelButton)
    {
        nextLevelSceneName = _nextLevelSceneName;
        actualScene = _actualScene;
        sceneNameComplete = _sceneNameComplete;
        showNextLevelButton = _showNextLevelButton;
        textButton = _textButton;
        fontSize = 100;
    }
    public LvlVariable(string _nextLevelSceneName, string _actualScene, string _sceneNameComplete,
    string _textButton, int _fontSize, bool _showNextLevelButton)
    {
        nextLevelSceneName = _nextLevelSceneName;
        actualScene = _actualScene;
        sceneNameComplete = _sceneNameComplete;
        showNextLevelButton = _showNextLevelButton;
        textButton = _textButton;
        fontSize = _fontSize;
    }


    #endregion
    #region Funkcje zwracaj¹ce wartoœæ
    /// <summary>
    /// Zwraca gdzie zapisany w PlayerPrefs jest nastêpny poziom
    /// </summary>
    /// <returns></returns>
    public string GetNextLevelSceneName
    {
        get { return nextLevelSceneName; }
    }
    /// <summary>
    /// Zwraca nazwê wczytanej sceny
    /// </summary>
    /// <returns></returns>
    public string GetActualScene
    {
        get { return actualScene; }
    }
    /// <summary>
    /// Zwraca nazwê sceny do wyœwietlenia
    /// Na koniec poziomu
    /// </summary>
    /// <returns></returns>
    public string GetSceneNameComplete
    {
        get { return sceneNameComplete; }
    }
    /// <summary>
    /// Zwraca napis jaki ma siê wyœwietlaæ na przycisku
    /// </summary>
    /// <returns></returns>
    public string GetTextButton
    {
        get { return textButton; }
    }
    /// <summary>
    /// Zwraca wielkoœæ tekstu jaka ma byæ w New Game
    /// </summary>
    /// <returns></returns>
    public int GetFontSize
    {
        get { return fontSize; }
    }
    /// <summary>
    /// Czy ma wyœwietliæ przycisk
    /// </summary>
    /// <returns></returns>
    public bool GetShowNextLevelButton
    {
        get { return showNextLevelButton; }
    }
    #endregion
}
