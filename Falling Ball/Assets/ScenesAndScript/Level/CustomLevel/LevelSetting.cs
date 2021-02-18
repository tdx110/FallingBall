using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class LevelSetting : MonoBehaviour
{
  
    #region Obiekty
    public GameObject gameObjectMainCamera;
    public GameObject gameObjectBackgroundImage;
    public GameObject gameObjectMainMenuCanvas;
    public GameObject gameObjectPlayerUiCanvas;
    public GameObject gameObjectPauseCanvas;
    public GameObject gameObjectLevelCompleteCanvas;
    public GameObject gameObjectGameOverCanvas;
    public GameObject gameObjectCanvasControl;
    public GameObject gameObjectlLeftButton;
    public GameObject gameObjectlRightButton;
    public GameObject gameObjectScallingObject;
    #endregion
    #region Variable
    public bool GlobalMoveMine = false;
    public bool GlobalMoveIsland = false;
    #endregion
}
