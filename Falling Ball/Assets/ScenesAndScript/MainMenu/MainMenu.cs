using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    [Header("Canvas Nowa Gra")]
    private GameObject NewGameCanvas;
    [SerializeField]
    [Header("Canvas Start")]
    private GameObject StartCanvas;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt(GlobalVariable.lvlVariablesData[0].GetActualScene, 1);
        PlayerPrefs.GetInt(GlobalVariable.GoldPlayerPrefs,0);
    }

    public void NewGameButton()
    {
        StartCanvas.SetActive(false);
        NewGameCanvas.SetActive(true);
    }
    public void BackButton()
    {
        NewGameCanvas.SetActive(false);
        StartCanvas.SetActive(true);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
