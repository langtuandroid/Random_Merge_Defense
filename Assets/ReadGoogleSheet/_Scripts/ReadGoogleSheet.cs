using System;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using UnityEngine;
using DataTable;
[CreateAssetMenu(fileName = "ReadGoogleSheet", menuName = "ReadGoogleSheet/ReadGoogleSheet", order = 0)]
public class ReadGoogleSheet : ScriptableObject
{
#if UNITY_EDITOR
    public TextAsset googleJson;
    public string dataTableFolderPath;

    [System.Serializable]
    public class ClientInfo
    {
        [System.Serializable]
        public class Info
        {
            public string client_id;
            public string client_secret;
        }
        public Info installed;
    }
    public string speardSheetId;
    public string testSpeardSheetId;

    bool testMode;
    string inputKey;
    string url = "https://docs.google.com/spreadsheets/d/";
    public void ReadSheetData(bool testMode)
    {
        ClientInfo clientInfo = JsonUtility.FromJson<ClientInfo>(googleJson.text);

        var pass = new ClientSecrets();
        pass.ClientId = clientInfo.installed.client_id;
        pass.ClientSecret = clientInfo.installed.client_secret;
        this.testMode = testMode;
        var scopes = new string[] { SheetsService.Scope.SpreadsheetsReadonly };
        var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(pass, scopes, pass.ClientId, CancellationToken.None).Result;

        var service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
        });

        //테스트 시트 구분
        if (testMode)
        {
            inputKey = testSpeardSheetId;
        }
        else
        {
            inputKey = speardSheetId;
        }
        var request = service.Spreadsheets.Get(inputKey).Execute();
        foreach (Sheet sheet in request.Sheets)
        {
            SendRequest(service, sheet.Properties.Title);
        }

    }
    private void SendRequest(SheetsService service, string sheetName)
    {
        bool success = true;

        try
        {
            //!A1:Z은 스프레드시트 A열부터 Z열까지 데이터를 받아오겠다는 소리
            var request = service.Spreadsheets.Values.Get(inputKey, sheetName + "!A1:AZ");
            //API 호출로 받아온 IList 데이터
            var jsonObject = request.Execute().Values;
            // jsonObject.RemoveAt(1);
            //IList 데이터를 jsonConvert 하기위해 직렬화
            string jsonString = ParseSheetData(jsonObject);
            //데이터 테이블 적용
            DataRead(sheetName, jsonString);
        }
        catch (Exception e)
        {
            success = false;
            Debug.LogError(e);
        }
        if (!testMode)
            Debug.Log(sheetName + " 스프레드시트 로드 " + (success ? "성공" : "실패"));
        else
            Debug.Log("TEST : " + sheetName + " 스프레드시트 로드 " + (success ? "성공" : "실패"));
    }
    void DataRead(string sheetName, string jsonString)
    {
        DataTableSO dataTable = AssetDatabase.LoadAssetAtPath(string.Format($"{dataTableFolderPath}/{sheetName}.asset"), typeof(ScriptableObject)) as DataTableSO;
        dataTable.Read(jsonString);
    }

    private string ParseSheetData(IList<IList<object>> value)
    {
        StringBuilder jsonBuilder = new StringBuilder();

        IList<object> columns = value[0];

        jsonBuilder.Append("[");
        for (int row = 2; row < value.Count; row++)
        {
            var data = value[row];
            jsonBuilder.Append("{");
            for (int col = 0; col < data.Count; col++)
            {
                jsonBuilder.Append("\"" + columns[col] + "\"" + ":");
                jsonBuilder.Append("\"" + data[col] + "\"");
                jsonBuilder.Append(",");
            }
            jsonBuilder.Append("}");
            if (row != value.Count - 1)
                jsonBuilder.Append(",");
        }
        jsonBuilder.Append("]");
        return jsonBuilder.ToString();
    }
    public void OpenBuildSheet()
    {
        Application.OpenURL(url + speardSheetId);
    }
    public void OpenTestSheet()
    {
        Application.OpenURL(url + testSpeardSheetId);
    }
#endif
}