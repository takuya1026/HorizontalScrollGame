
//============================================================
// @file EditorBlockBase
// @brief ブロックベースのエディタ拡張
// @autor ochi takuya
//============================================================

using UnityEditor;

/// <summary>
/// ブロックベースのエディタ拡張
/// </summary>
[CustomEditor(typeof(BlockBase), true)]
public class EditorBlockBase : Editor
{
    private BlockBase _target;

    /// <summary>
    /// 開始
    /// </summary>
    private void Awake()
    {
        _target = target as BlockBase;
    }

    /// <summary>
    /// インスペクターの変更
    /// </summary>
    public override void OnInspectorGUI()
    {
        _target.m_EnumBlockType = (EnumBlockType)EditorGUILayout.EnumPopup("Enum Block Type", _target.m_EnumBlockType);
        if (_target.IsItemAndCoinBlock())
        {
            _target.m_ItemId = EditorGUILayout.DelayedIntField("Item Id", _target.m_ItemId);
            _target.m_Quantity = EditorGUILayout.DelayedIntField("Quantity", _target.m_Quantity);
        }
        //base.OnInspectorGUI();
    }
}
