using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(UiButtonLevelScript))]
public class UiButtonLevelInspector : Editor
{
    private bool showObjectList = true;

    public override void OnInspectorGUI()
    {
        UiButtonLevelScript uiButtonScript = (UiButtonLevelScript)target;

        #region Rozwijane Obiekty
        showObjectList = EditorGUILayout.BeginFoldoutHeaderGroup(showObjectList, "Obiekty");
        if (showObjectList)
        {
            GUILayout.Label("G³ówny przycisk.");
            uiButtonScript.ButtonLevel = (Button)EditorGUILayout.ObjectField(uiButtonScript.ButtonLevel, typeof(Button), true);
            GUILayout.Label("Pole tekstowe przycisku.");
            uiButtonScript.TextButton = (Text)EditorGUILayout.ObjectField(uiButtonScript.TextButton, typeof(Text), true);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        #endregion

        GUILayout.Label("Poziom do obs³ugi przez przycisk");
        GUILayout.Label("Lista poziomów zapisana w GlobalVariable");
        uiButtonScript.LvlSelect = (GlobalVariable.LvlList)
            EditorGUILayout.EnumPopup("Level: ", uiButtonScript.LvlSelect);
        uiButtonScript.SetButton();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(uiButtonScript);
            EditorSceneManager.MarkSceneDirty(uiButtonScript.gameObject.scene);
            PrefabUtility.RecordPrefabInstancePropertyModifications(uiButtonScript.gameObject.transform);
        }
    }
}