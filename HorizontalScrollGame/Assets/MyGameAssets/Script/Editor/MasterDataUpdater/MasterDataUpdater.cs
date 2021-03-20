using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using System.Threading;
using Google.Apis.Services;
using System;
using System.Linq;
using Google.Apis.Sheets.v4.Data;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

/// <summary>
/// マスターデータの更新を行うウィンドウ
/// </summary>
public class MasterDataUpdater : EditorWindow
{
    /// <summary>
    /// ユーザー情報
    /// </summary>
    private UserCredential m_userCredential;

    /// <summary>
    /// スプレッドシートのAPI
    /// </summary>
    private SheetsService m_sheetsService;

    [MenuItem("Window/MasterData/MasterDataUpdater")]
    private static void Open()
    {
        // 生成
        GetWindow<MasterDataUpdater>("MasterDataUpdater");
    }

    /// <summary>
    /// GUI
    /// </summary>
    private async void OnGUI()
    {
        GUILayout.Space(10);
        if (GUILayout.Button("ローカルのマスターデータを更新する"))
        {
            if (EditorUtility.DisplayDialog("マスターデータ更新", "ローカルのマスターデータを更新しますか？", "はい", "いいえ"))
            {
                // 認証
                if (m_userCredential == null)
                {
                    m_userCredential = await AuthorizeAsync();
                }

                // Serviceを初期化
                if (m_sheetsService == null)
                {
                    m_sheetsService = new SheetsService(new BaseClientService.Initializer
                    {
                        HttpClientInitializer = m_userCredential
                    });
                }

                // 各マスターデータのIDを取得
                string jsonText = File.ReadAllText("Assets/MyGameAssets/Data/MasterData/master_sheetIdList.json");
                Dictionary<string,string> masterIdTable = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonText);

                foreach (var masterId in masterIdTable.Values)
                {
                    // スプレッドシートの内容をJson形式に変換してファイル出力
                    var response = await SheetToJsonFormat(masterId);
                    File.WriteAllText($"../HorizontalScrollGame.Server/Master/{response.Item1}.json", response.Item2, Encoding.UTF8);
                    EnemyMaster[] masters = JsonConvert.DeserializeObject<EnemyMaster[]>(response.Item2);
                }

                // 完了したよ！
                EditorUtility.DisplayDialog("更新完了", "更新が完了しました。", "OK");
            }
        }
    }

    /// <summary>
    /// OAurth認証
    /// </summary>
    public static async Task<UserCredential> AuthorizeAsync()
    {
        // APIのキー情報取得
        string secret = File.ReadAllText("Packages/client_secret.json", Encoding.UTF8);
        JObject secretData = JObject.Parse(secret);
        string clientId = (string)secretData["installed"]["client_id"];
        string clientSecret = (string)secretData["installed"]["client_secret"];
        var user = $"{Application.productName}_{Environment.UserName}";

        var secrets = new ClientSecrets
        {
            ClientId = clientId,
            ClientSecret = clientSecret,
        };

        var scopes = new[]
        {
            SheetsService.Scope.SpreadsheetsReadonly
        };
        return await GoogleWebAuthorizationBroker.AuthorizeAsync(secrets, scopes, user, CancellationToken.None);
    }

    

    /// <summary>
    /// スプレッドシートをJSON形式に変換
    /// </summary>
    public async Task<(string,string)> SheetToJsonFormat(string spreadsheetId)
    {
        // シートを読み込む
        var spreadsheets = m_sheetsService.Spreadsheets;
        Spreadsheet spreadsheet = await spreadsheets.Get(spreadsheetId).ExecuteAsync();
        var sheetProperties = spreadsheet.Sheets.First().Properties;
        var gridProperties = sheetProperties.GridProperties;

        // 変数名を取得
        var paramTypeNameResponse = await spreadsheets.Values.Get(spreadsheetId,$"{sheetProperties.Title}!B3:3").ExecuteAsync();
        var paramTypeNames = paramTypeNameResponse.Values.First();

        // 各種パラメータ取得
        string range = $"{sheetProperties.Title}!B5:{SpreadsheetUtility.ColumnIndexToAlphabet(gridProperties.ColumnCount.Value-1)}{gridProperties.RowCount}";
        var paramResponse = await spreadsheets.Values.Get(spreadsheetId, range).ExecuteAsync();
        var parameters = paramResponse.Values;

        // NOTE : 変数名いろいろと適当だけど機能的には問題ないからヨシ！

        // データテーブル作成
        List<Dictionary<string, string>> parameterTable = new List<Dictionary<string, string>>();
        foreach (var element in parameters)
        {
            int count = 0;
            var table = new Dictionary<string, string>();
            foreach (var parameter in element)
            {
                table.Add(paramTypeNames[count].ToString(), parameter.ToString());
                count++;
            }
            parameterTable.Add(table);
        }

        // テーブルをJSON形式に変換
        return (spreadsheet.Properties.Title, JsonConvert.SerializeObject(parameterTable));
    }
}
