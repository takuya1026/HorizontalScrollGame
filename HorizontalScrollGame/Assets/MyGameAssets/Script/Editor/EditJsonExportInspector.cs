
//============================================================
// @file EditJsonExportInspector
// @brief Json出力のエディタ拡張
// @autor ochi takuya
//============================================================

using UnityEditor;
using UnityEngine;

/// <summary>
/// Json出力のエディタ拡張
/// </summary>
[CustomEditor(typeof(JsonExport))]
public class EditJsonExportInspector : Editor
{
    private JsonExport _target;

    private void Awake()
    {
        _target = target as JsonExport;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("書き出し"))
        {
            _target.SeavStageInfo();
        }
    }
}