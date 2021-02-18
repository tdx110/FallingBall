using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class LevelSetting : MonoBehaviour
{
    #region Variable
    public bool GlobalMoveIsland = false;
    public bool GlobalMoveMine = false;
    #endregion
    #region Obiekty
    public GameObject gameObjectMainCamera;
    public GameObject gameObjectBackgroundImage;
    public GameObject gameObjectMainMenuCanvas;
    public GameObject gameObjectPlayerUiCanvas;
    public GameObject gameObjectPauseCanvas;
    public GameObject gameObjectLevelCompleteCanvas;
    public GameObject gameObjectNextLevel;
    public GameObject gameObjectGameOverCanvas;
    public GameObject gameObjectControlCanvas;
    public GameObject gameObjectScallingObject;
    #endregion
}
