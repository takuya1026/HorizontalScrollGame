
//============================================================
// @file EditJsonImportInspector
// @brief Json入力のエディタ拡張
// @autor ochi takuya
//============================================================

using UnityEditor;
using UnityEngine;

/// <summary>
/// Json入力のエディタ拡張
/// </summary>
[CustomEditor(typeof(JsonImport))]
public class EditJsonImportInspector : Editor
{
    private JsonImport _target;

    /// <summary>
    /// 開始
    /// </summary>
    private void Awake()
    {
        _target = target as JsonImport;
    }

    /// <summary>
    /// インスペクターの変更
    /// </summary>
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("読み込み"))
        {
            _target.GetStageInfo(StageGenerator.m_Instance.Generate);
        }
    }
}
