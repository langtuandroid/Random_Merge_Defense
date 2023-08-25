using UnityToolbarExtender;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class DataTableReadToolBar
{
    static DataTableReadToolBar()
    {
        ToolbarExtender.RightToolbarGUI.Add(DataTableSet);
    }

    private static void DataTableSet()
    {
        GUILayout.FlexibleSpace();

        var setting = Resources.Load("ReadGoogleSheet") as ReadGoogleSheet;

        if (GUILayout.Button(new GUIContent("TestDataTable", "TestDataTable 적용"), GUILayout.Width(100f)))
        {
            Error(setting);
            setting.ReadSheetData(true);
        }
        if (GUILayout.Button(new GUIContent("BuildDataTable", "BuildDataTable 적용"), GUILayout.Width(100f)))
        {
            Error(setting);
            setting.ReadSheetData(false);
        }
        if (GUILayout.Button(new GUIContent("OpenTestSheet", "시트 열기"), GUILayout.Width(100f)))
        {
            Error(setting);
            setting.OpenTestSheet();
        }
        if (GUILayout.Button(new GUIContent("OpenBuildSheet", "시트 열기"), GUILayout.Width(100f)))
        {
            Error(setting);
            setting.OpenBuildSheet();
        }
    }
    static void Error(ReadGoogleSheet setting)
    {
        var errorLogs = new List<string>();
        if (string.IsNullOrEmpty(setting.dataTableFolderPath)) errorLogs.Add("folderPath가 입력되지 않았습니다.");
        if (setting.googleJson == null) errorLogs.Add("googleJson이 설정되지 않았습니다.");
        if (string.IsNullOrEmpty(setting.speardSheetId)) errorLogs.Add("speardSheetId가 입력되지 않았습니다.");
        if (string.IsNullOrEmpty(setting.testSpeardSheetId)) errorLogs.Add("testSpeardSheetId가 입력되지 않았습니다.");
        if (errorLogs.Count > 0)
        {
            var errorLog = "ReadGoogleSheet에서 다음과 같은 문제가 발견되었습니다:\n";
            foreach (var log in errorLogs)
            {
                errorLog += $"\n{log}";
            }

            if (EditorUtility.DisplayDialog("Warnings", errorLog, "OK"))
            {
                throw new Exception("취소");
            }
        }
    }

}
