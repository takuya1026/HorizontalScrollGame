
//============================================================
// @file EditorPrefabPlacementFolder
// @brief プレハブの配置を指定フォルダ内にするのエディタ拡張
// @autor ochi takuya
//============================================================

using UnityEditor;
using UnityEngine;
using Hexat.Editor;

/// <summary>
/// プレハブの配置を指定フォルダ内にするのエディタ拡張
/// </summary>
[CustomEditor(typeof(PrefabPlacementFolder))]
public class EditorPrefabPlacementFolder : Editor
{
    private PrefabPlacementFolder _target;

    /// <summary>
    /// 開始
    /// </summary>
    private void Awake()
    {
        _target = target as PrefabPlacementFolder;
    }

    /// <summary>
    /// インスペクターの変更
    /// </summary>
    public override void OnInspectorGUI()
    {
        _target.m_IsEnableParentSearchByID = EditorGUILayout.ToggleLeft("Is Enable Parent Search By ID", _target.m_IsEnableParentSearchByID);
        if (_target.m_IsEnableParentSearchByID)
        {
            EditorGUILayout.LabelField("親のIDで検索 [ Self Name + _Element_ + Parent Id ]");
            _target.m_ParentId = EditorGUILayout.IntField("Parent ID", _target.m_ParentId);

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Next"))
                {
                    _target.m_ParentId++;
                }

                if (GUILayout.Button("Prev"))
                {
                    _target.m_ParentId--;
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.LabelField("親の名前で検索");
            _target.m_ParentName = EditorGUILayout.TextField("Parent Name", _target.m_ParentName);
        }
    }
}
