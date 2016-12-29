using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


[CustomEditor(typeof(GameManager))]
public class GameManagerInspector : Editor
{
    private static bool mEndHorizontal = false;
    private GameManager mGameManager = null;

    public static bool DrawHeader(string text) { return DrawHeader(text, text, false, false); }

    void OnEnable()
    {
        if (mGameManager == null)
            mGameManager = target as GameManager;
    }

    public override void OnInspectorGUI()
    {
        if (DrawHeader("각종 기능들"))
        {
            BeginContents(true);

            EditorGUILayout.BeginHorizontal();
            
            GUI.changed = false;
            mGameManager.isDebugLog = EditorGUILayout.Toggle("Use Debug Log", mGameManager.isDebugLog);

            SetDefineSymbol("ENABLE_LOG", mGameManager.isDebugLog);

            if (GUI.changed && false == Application.isPlaying)
                UnityEditor.SceneManagement.EditorSceneManager.SaveOpenScenes();

            EditorGUILayout.EndHorizontal();

            EndContents();
        }
    }

    #region Tool
    public static void SetDefineSymbol(string define, bool isOn)
    {
        string[] defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup).Split(';');
        bool isEnableLog = false;        
        for (int i = 0; i < defines.Length; i++)
        {
            if (defines[i].Equals("ENABLE_LOG"))
            {
                isEnableLog = true;
            }
        }

        if (isOn)
        {
            if (!isEnableLog)
            {
                string _defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup) + ";ENABLE_LOG";
                PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, _defines);
            }
        }
        else
        {
            if (isEnableLog)
            {
                string _defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup).Replace(";ENABLE_LOG", "");
                PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, _defines);
            }
        }
    }
    public static bool DrawHeader(string text, string key, bool forceOn, bool minimalistic)
    {
        bool state = EditorPrefs.GetBool(key, true);

        if (!minimalistic) GUILayout.Space(3f);
        if (!forceOn && !state) GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
        GUILayout.BeginHorizontal();
        GUI.changed = false;

        if (minimalistic)
        {
            if (state) text = "\u25BC" + (char)0x200a + text;
            else text = "\u25BA" + (char)0x200a + text;

            GUILayout.BeginHorizontal();
            GUI.contentColor = EditorGUIUtility.isProSkin ? new Color(1f, 1f, 1f, 0.7f) : new Color(0f, 0f, 0f, 0.7f);
            if (!GUILayout.Toggle(true, text, "PreToolbar2", GUILayout.MinWidth(20f))) state = !state;
            GUI.contentColor = Color.white;
            GUILayout.EndHorizontal();
        }
        else
        {
            text = "<b><size=11>" + text + "</size></b>";
            if (state) text = "\u25BC " + text;
            else text = "\u25BA " + text;
            if (!GUILayout.Toggle(true, text, "dragtab", GUILayout.MinWidth(20f))) state = !state;
        }

        if (GUI.changed) EditorPrefs.SetBool(key, state);

        if (!minimalistic) GUILayout.Space(2f);
        GUILayout.EndHorizontal();
        GUI.backgroundColor = Color.white;
        if (!forceOn && !state) GUILayout.Space(3f);
        return state;
    }
    public static void BeginContents(bool minimalistic)
    {
        if (!minimalistic)
        {
            mEndHorizontal = true;
            GUILayout.BeginHorizontal();
            EditorGUILayout.BeginHorizontal("AS TextArea", GUILayout.MinHeight(10f));
        }
        else
        {
            mEndHorizontal = false;
            EditorGUILayout.BeginHorizontal(GUILayout.MinHeight(10f));
            GUILayout.Space(10f);
        }
        GUILayout.BeginVertical();
        GUILayout.Space(2f);
    }
    public static void EndContents()
    {
        GUILayout.Space(3f);
        GUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        if (mEndHorizontal)
        {
            GUILayout.Space(3f);
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(3f);
    }
    #endregion
}
