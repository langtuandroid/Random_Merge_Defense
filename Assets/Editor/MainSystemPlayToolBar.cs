using UnityToolbarExtender;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class MainSystemPlayToolBar
{
    static string PreviousScene
    {
        get => EditorPrefs.GetString("PreviousScene", SceneManager.GetActiveScene().path);
        set => EditorPrefs.SetString("PreviousScene", value);
    }

    static MainSystemPlayToolBar()
    {
        ToolbarExtender.LeftToolbarGUI.Add(InitPlayWithMainSystemButton);
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    static class ToolbarStyles
    {
        public static readonly GUIStyle commandButtonStyle;

        static ToolbarStyles()
        {
            commandButtonStyle = new GUIStyle("Command")
            {
                fontSize = 16,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove,
                fontStyle = FontStyle.Bold
            };
        }
    }

    static void InitPlayWithMainSystemButton()
    {
        GUILayout.FlexibleSpace();
        if (GUILayout.Button(new GUIContent("P", "Play With MainSystem"), ToolbarStyles.commandButtonStyle))
        {
            OnPlayWithMainSystemButton();
        }
    }

    static void OnPlayWithMainSystemButton()
    {
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
            return;
        }

        PreviousScene = SceneManager.GetActiveScene().path;

        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/_Scenes/Title.unity");
        EditorApplication.isPlaying = true;
    }

    static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredEditMode)
        {
            if (IsOpenablePriviousScene())
            {
                if (SceneManager.GetActiveScene().path == PreviousScene) return;
                // User pressed stop -- reload previous scene.
                try
                {
                    EditorSceneManager.OpenScene(PreviousScene);
                }
                catch
                {
                    Debug.LogError($"error: scene not found: {PreviousScene}");
                }
            }
        }

        bool IsOpenablePriviousScene()
        {
            return !EditorApplication.isPlaying
                   && !EditorApplication.isPlayingOrWillChangePlaymode
                   && !string.IsNullOrWhiteSpace(PreviousScene);
        }
    }
}
