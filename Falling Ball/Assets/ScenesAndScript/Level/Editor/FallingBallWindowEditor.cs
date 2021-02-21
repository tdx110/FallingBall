using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.Events;
using UnityEditor.SceneManagement;

public class FallingBallWindowEditor : EditorWindow
{
    #region Edytor Window
    private LevelSetting levelSetting;
    private MainCamera mainCamera;
    private Rect windowEditorSize;
    private GUIStyle HeadLineText;

    #endregion
    #region Camera Variable
    private float speedCamera = 1;
    private int stepCamera = 1;
    #endregion
    #region MineVariable
    private float speedMine = 0;
    private int stepMine = 1;
    #endregion
    #region BallVariable
    private int forceX;
    #endregion
    #region IslandVariable
    private float speedIsland = 0;
    private int stepIsland = 1;
    #endregion
    #region Variable Level
    private addTypeObject typeObject = addTypeObject.None;
    private Vector2 scrollPositionIsland = Vector2.zero;
    private Vector2 scrollPositionEnemy = Vector2.zero;
    private Sprite[] spriteArray = null;
    private Texture island1 = null;
    private Texture island2 = null;
    private Texture island3 = null;
    private Texture mine = null;
    private Texture ball = null;
    #endregion


    [MenuItem("Window/Falling Ball Level Editor")]
    public static void ShowWindow()
    {
        GetWindow<FallingBallWindowEditor>("Falling Ball Level Editor");
    }

    private void OnGUI()
    {
        //Ustawienia tekstu
        HeadLineText = new GUIStyle("Label");
        HeadLineText.alignment = TextAnchor.MiddleCenter;
        HeadLineText.fontSize = 20;
        HeadLineText.fontStyle = FontStyle.Bold;
        //Wymiary edytora
        windowEditorSize = position;
        //////////Tylko dla programisty do resetowanie zmiennych!
        if (GUILayout.Button("Reset all inside variable (ONLY programmer)"))
        {
            typeObject = addTypeObject.None;
            spriteArray = null;
            island1 = null;
            island2 = null;
            island3 = null;
            mine = null;
        }
        //Jeœli jesteœmy w scenie która ma wyra¿enie w nazwie "Lvl"
        string nazwaSceny = SceneManager.GetActiveScene().name;
        if (nazwaSceny.Contains("Lvl") == true)
        {
            levelSetting = GameObject.Find("LevelSetting").GetComponent<LevelSetting>();
            mainCamera = levelSetting.gameObjectMainCamera.GetComponent<MainCamera>();
            showCameraSetting();
            showAddObject();
        }
        else
        {
            EditorGUILayout.LabelField("Editor availabe only Level Scene.", HeadLineText);
        }
        if (Selection.activeObject)
        {
            if (Selection.activeObject.name.Contains("Island"))
            {
                if (Selection.activeGameObject != null) selectIsland();
            }
            else if (Selection.activeObject.name.Contains("Mine"))
            {
                if (Selection.activeGameObject != null) selectMine();
            }
            else if (Selection.activeObject.name.Contains("Ball"))
            {
                if (Selection.activeGameObject != null) selectBall();
            }
        }
    }
    private void Update()
    {

    }
    //////////////////////////////////////////////////
    //////////////////////////////////////////////////
    //////////////////////////////////////////////////

    private void showCameraSetting()
    {
        Image imageBackground = levelSetting.gameObjectBackgroundImage.GetComponent<Image>();

        if (mainCamera.Position.Count == 0)
        {
            mainCamera.Position.Add(levelSetting.gameObjectMainCamera.transform.position);
            mainCamera.Speed.Add(speedCamera);
            mainCamera.ColorsBackground.Add(imageBackground.color);
            stepCamera = 1;
        }

        GUILayout.Space(20);
        EditorGUILayout.LabelField("Camera settings:", HeadLineText);
        GUILayout.Label("Camera move count: " + mainCamera.Position.Count);
        GUILayout.Label("Actual parameter step: " + stepCamera);
        speedCamera = EditorGUILayout.Slider(speedCamera, 0.2f, 3);
        imageBackground.color = EditorGUILayout.ColorField(imageBackground.color);

        #region Button
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add position"))
        {
            mainCamera.Position.Add(levelSetting.gameObjectMainCamera.transform.position);
            mainCamera.Speed.Add(speedCamera);
            mainCamera.ColorsBackground.Add(imageBackground.color);
            stepCamera = mainCamera.Position.Count;
        }
        if (GUILayout.Button("Remove position"))
        {
            mainCamera.Position.RemoveAt(mainCamera.Position.Count - 1);
            mainCamera.Speed.RemoveAt(mainCamera.Speed.Count - 1);
            mainCamera.ColorsBackground.RemoveAt(mainCamera.ColorsBackground.Count - 1);
            if (stepCamera > mainCamera.Position.Count) stepCamera = mainCamera.Position.Count;
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Edit position"))
        {
            mainCamera.Position[stepCamera - 1] = levelSetting.gameObjectMainCamera.transform.position;
            mainCamera.Speed[stepCamera - 1] = speedCamera;
            mainCamera.ColorsBackground[stepCamera - 1] = imageBackground.color;
        }
        if (GUILayout.Button("Clear all position"))
        {
            mainCamera.Position.Clear();
            mainCamera.Speed.Clear();
            mainCamera.ColorsBackground.Clear();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Prev. position"))
        {
            if (stepCamera == 1) stepCamera = mainCamera.Position.Count;
            else stepCamera = stepCamera - 1;

            levelSetting.gameObjectMainCamera.transform.position = mainCamera.Position[stepCamera - 1];
            speedCamera = mainCamera.Speed[stepCamera - 1];
            imageBackground.color = mainCamera.ColorsBackground[stepCamera - 1];
        }
        if (GUILayout.Button("Next position"))
        {
            if (stepCamera == mainCamera.Position.Count) stepCamera = 1;
            else stepCamera = stepCamera + 1;

            levelSetting.gameObjectMainCamera.transform.position = mainCamera.Position[stepCamera - 1];
            speedCamera = mainCamera.Speed[stepCamera - 1];
            imageBackground.color = mainCamera.ColorsBackground[stepCamera - 1];
        }
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Select Camera"))
        {
            Selection.activeObject = levelSetting.gameObjectMainCamera;
        }
        #endregion

        if (GUI.changed)
        {
            EditorUtility.SetDirty(mainCamera);
            EditorSceneManager.MarkSceneDirty(mainCamera.gameObject.scene);
            PrefabUtility.RecordPrefabInstancePropertyModifications(mainCamera.gameObject.transform);
        }
    }
    private void showAddObject()
    {
        GUILayout.Space(20);
        GUILayout.Label("Object Setting:", HeadLineText);
        typeObject = (addTypeObject)EditorGUILayout.EnumPopup("Object type:", typeObject);
        if (typeObject == addTypeObject.Island) drawIsland();
        if (typeObject == addTypeObject.Other) drawOther();
        if (typeObject == addTypeObject.Enemy) drawEnemy();
    }
    #region Add Other Object
    private void drawIsland()
    {
        spriteArray = Resources.LoadAll<Sprite>("Texture/Object");
        GameObject gameObject = null;
        float ratio = 234;
        if (island1 == null || island2 == null || island3 == null)
        {
            foreach (Sprite item in spriteArray)
            {
                if (item.name == "Island 1") island1 = AssetPreview.GetAssetPreview(item);
                if (item.name == "Island 2") island2 = AssetPreview.GetAssetPreview(item);
                if (item.name == "Island 3") island3 = AssetPreview.GetAssetPreview(item);
            }
        }
        scrollPositionIsland = GUILayout.BeginScrollView(scrollPositionIsland, true, false,
            GUILayout.Width(Screen.width), GUILayout.Height(120));
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(island1, GUILayout.Width(150), GUILayout.Height(100)))
        {
            gameObject = (GameObject)Instantiate((GameObject)Resources.Load("GameObject/Island/Island 1"));
            gameObject.transform.SetParent(levelSetting.gameObjectScallingObject.transform, false);
            gameObject.GetComponent<IslandScript>().levelSetting = levelSetting;
            Vector3 vector3 = levelSetting.gameObjectMainCamera.transform.localPosition;
            vector3 = new Vector3(vector3.x * ratio, vector3.y * ratio, 0);
            gameObject.transform.localPosition = vector3;
            Selection.activeObject = gameObject;
        }
        if (GUILayout.Button(island2, GUILayout.Width(150), GUILayout.Height(100)))
        {
            gameObject = (GameObject)Instantiate((GameObject)Resources.Load("GameObject/Island/Island 2"));
            gameObject.transform.SetParent(levelSetting.gameObjectScallingObject.transform, false);
            gameObject.GetComponent<IslandScript>().levelSetting = levelSetting;
            Vector3 vector3 = levelSetting.gameObjectMainCamera.transform.localPosition;
            vector3 = new Vector3(vector3.x * ratio, vector3.y * ratio, 0);
            gameObject.transform.localPosition = vector3;
            Selection.activeObject = gameObject;
        }
        if (GUILayout.Button(island3, GUILayout.Width(150), GUILayout.Height(100)))
        {
            gameObject = (GameObject)Instantiate((GameObject)Resources.Load("GameObject/Island/Island 3"));
            gameObject.transform.SetParent(levelSetting.gameObjectScallingObject.transform, false);
            gameObject.GetComponent<IslandScript>().levelSetting = levelSetting;
            Vector3 vector3 = levelSetting.gameObjectMainCamera.transform.localPosition;
            vector3 = new Vector3(vector3.x * ratio, vector3.y * ratio, 0);
            gameObject.transform.localPosition = vector3;
            Selection.activeObject = gameObject;
        }

        GUILayout.EndHorizontal();
        GUILayout.EndScrollView();

    }
    private void drawOther()
    {
        spriteArray = Resources.LoadAll<Sprite>("Texture/Object");
        if (mine == null)
        {
            foreach (Sprite item in spriteArray)
            {
                if (item.name == "Ball") ball = AssetPreview.GetAssetPreview(item);
            }
        }
        scrollPositionEnemy = GUILayout.BeginScrollView(scrollPositionEnemy, false, true,
            GUILayout.Width(Screen.width), GUILayout.Height(120));
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(ball, GUILayout.Width(150), GUILayout.Height(100)))
        {
            float ratio = 234;
            GameObject gameObject = (GameObject)Instantiate((GameObject)Resources.Load("GameObject/Player/Ball"));
            gameObject.transform.SetParent(levelSetting.gameObjectScallingObject.transform, false);
            gameObject.GetComponent<BallScript>().levelSetting = levelSetting;
            gameObject.GetComponent<BallScript>().mainMenuCustomEditor =
                levelSetting.gameObjectMainMenuCanvas.GetComponent<MainMenuCustomEditor>();
            Vector3 vector3 = levelSetting.gameObjectMainCamera.transform.localPosition;
            vector3 = new Vector3(vector3.x * ratio, vector3.y * ratio, 0);
            gameObject.transform.localPosition = vector3;
            Selection.activeObject = gameObject;

        }
        GUILayout.EndHorizontal();
        GUILayout.EndScrollView();
    }
    private void drawEnemy()
    {
        spriteArray = Resources.LoadAll<Sprite>("Texture/Object");
        if (mine == null)
        {
            foreach (Sprite item in spriteArray)
            {
                if (item.name == "Mine") mine = AssetPreview.GetAssetPreview(item);
            }
        }
        scrollPositionEnemy = GUILayout.BeginScrollView(scrollPositionEnemy, false, true,
            GUILayout.Width(Screen.width), GUILayout.Height(120));
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(mine, GUILayout.Width(150), GUILayout.Height(100)))
        {
            float ratio = 234;
            GameObject gameObject = (GameObject)Instantiate((GameObject)Resources.Load("GameObject/Enemy/Mine"));
            gameObject.transform.SetParent(levelSetting.gameObjectScallingObject.transform, false);
            gameObject.GetComponent<MineScript>().levelSetting = levelSetting;
            Vector3 vector3 = levelSetting.gameObjectMainCamera.transform.localPosition;
            vector3 = new Vector3(vector3.x * ratio, vector3.y * ratio, 0);
            gameObject.transform.localPosition = vector3;
            Selection.activeObject = gameObject;
        }
        GUILayout.EndHorizontal();
        GUILayout.EndScrollView();
    }
    #endregion

    #region Select Object Options
    private void selectIsland()
    {
        IslandScript islandScript = Selection.activeGameObject.GetComponent<IslandScript>();
        GameObject gameObject = Selection.activeGameObject;

        if (islandScript.Position.Count == 0)
        {
            islandScript.Position.Add(gameObject.transform.position);
            islandScript.Speed.Add(speedIsland);
            stepIsland = 1;
            speedIsland = 1;

        }

        GUILayout.Label("Island Options:", HeadLineText);
        GUILayout.Label("Steps: " + islandScript.Position.Count);
        GUILayout.Label("Actual steps: " + stepIsland);
        speedIsland = EditorGUILayout.Slider("Speed Island:", speedIsland, 0, 4);
        islandScript.ValueMoney = EditorGUILayout.IntSlider("How much Money:", islandScript.ValueMoney, 0, 10);
        islandScript.Moving = EditorGUILayout.Toggle("Enable move:", islandScript.Moving);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add position"))
        {
            islandScript.Position.Add(gameObject.transform.position);
            islandScript.Speed.Add(speedIsland);
            stepIsland++;
        }
        if (GUILayout.Button("Remove position"))
        {
            islandScript.Position.RemoveAt(islandScript.Position.Count - 1);
            islandScript.Speed.RemoveAt(islandScript.Speed.Count - 1);
            if (stepIsland > 0) stepIsland = 0;
            else stepIsland = 1;

        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Edit position"))
        {
            islandScript.Position[stepIsland - 1] = gameObject.transform.position;
            islandScript.Speed[stepIsland - 1] = speedIsland;
        }
        if (GUILayout.Button("Clear all position"))
        {
            islandScript.Position.Clear();
            islandScript.Speed.Clear();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Prev. position"))
        {
            if (stepIsland == 1)
            {
                stepIsland = islandScript.Position.Count;
                gameObject.transform.position = islandScript.Position[stepIsland - 1];
                speedIsland = islandScript.Speed[stepIsland - 1];
            }
            else
            {
                stepIsland--;
                gameObject.transform.position = islandScript.Position[stepIsland - 1];
                speedIsland = islandScript.Speed[stepIsland - 1];
            }
        }
        if (GUILayout.Button("Next position"))
        {
            if (stepIsland == islandScript.Position.Count)
            {
                stepIsland = 1;
                gameObject.transform.position = islandScript.Position[stepIsland - 1];
                speedIsland = islandScript.Speed[stepIsland - 1];
            }
            else
            {
                stepIsland++;
                gameObject.transform.position = islandScript.Position[stepIsland - 1];
                speedIsland = islandScript.Speed[stepIsland - 1];
            }
        }
        EditorGUILayout.EndHorizontal();
    }
    private void selectBall()
    {
        GUILayout.Label("Ball Options:", HeadLineText);
    }
    private void selectMine()
    {
        MineScript mineScript = Selection.activeGameObject.GetComponent<MineScript>();
        GameObject gameObject = Selection.activeGameObject;

        if (mineScript.Position.Count == 0)
        {
            mineScript.Position.Add(gameObject.transform.position);
            mineScript.Speed.Add(speedMine);
            stepMine = 1;
            speedMine = 1;

        }

        GUILayout.Label("Mine Options:", HeadLineText);
        GUILayout.Label("Steps: " + mineScript.Position.Count);
        GUILayout.Label("Actual steps: " + stepMine);
        speedMine = EditorGUILayout.Slider("Speed Mine:", speedMine, 0, 4);
        mineScript.Moving = EditorGUILayout.Toggle("Enable move:", mineScript.Moving);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add position"))
        {
            mineScript.Position.Add(gameObject.transform.position);
            mineScript.Speed.Add(speedMine);
            stepMine++;
        }
        if (GUILayout.Button("Remove position"))
        {
            mineScript.Position.RemoveAt(mineScript.Position.Count - 1);
            mineScript.Speed.RemoveAt(mineScript.Speed.Count - 1);
            stepMine--;
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Edit position"))
        {
            mineScript.Position[stepMine - 1] = gameObject.transform.position;
            mineScript.Speed[stepMine - 1] = speedMine;
        }
        if (GUILayout.Button("Clear all position"))
        {
            mineScript.Position.Clear();
            mineScript.Speed.Clear();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Prev. position"))
        {
            if (stepMine == 1)
            {
                stepMine = mineScript.Position.Count;
                gameObject.transform.position = mineScript.Position[stepMine - 1];
                speedMine = mineScript.Speed[stepMine - 1];
            }
            else
            {
                stepMine--;
                gameObject.transform.position = mineScript.Position[stepMine - 1];
                speedMine = mineScript.Speed[stepMine - 1];
            }
        }
        if (GUILayout.Button("Next position"))
        {
            if (stepMine == mineScript.Position.Count)
            {
                stepMine = 1;
                gameObject.transform.position = mineScript.Position[stepMine - 1];
                speedMine = mineScript.Speed[stepMine - 1];
            }
            else
            {
                stepMine++;
                gameObject.transform.position = mineScript.Position[stepMine - 1];
                speedMine = mineScript.Speed[stepMine - 1];
            }
        }
        EditorGUILayout.EndHorizontal();
    }
    #endregion
    private enum addTypeObject
    {
        None = 0,
        Island = 1,
        Other = 2,
        Enemy = 3,
    }
}