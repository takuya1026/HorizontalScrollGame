
//============================================================
// @file EditorBlockInfo
// @brief ブロックの情報のエディタ拡張
// @autor ochi takuya
//============================================================
using UnityEditor;

/// <summary>
/// ブロックの情報のエディタ拡張
/// </summary>
[CustomEditor(typeof(BlockInfo))]
public class EditorBlockInfo : Editor
{
    private BlockInfo _target;

    private void Awake()
    {
        _target = target as BlockInfo;
    }

    public override void OnInspectorGUI()
    {
        _target.m_EnumBlockType = (EnumBlockType)EditorGUILayout.EnumPopup("Enum Block Type", _target.m_EnumBlockType);
        if (_target.IsItemAndCoinBlock())
        {
            _target.m_ItemId = EditorGUILayout.DelayedIntField("Item Id", _target.m_ItemId);
            _target.m_Quantity = EditorGUILayout.DelayedIntField("Quantity", _target.m_Quantity);
        }
    }
}
