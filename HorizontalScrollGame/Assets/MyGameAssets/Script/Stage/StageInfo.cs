
//============================================================
// @file StageInfo
// @brief ステージの情報
// @autor ochi takuya
//============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージの情報
/// </summary>
[System.Serializable]
public class StageInfo
{
    [System.Serializable]
    public struct ResultBlockInfo
    {
        public string m_parentName;
        public string m_name;
        public Vector3 m_postion;
        public Quaternion m_rotation;
        public Vector3 m_scale;
        public EnumBlockType m_enumBlockType;
        public int m_itemId;
        public int m_quantity;
        public GameObject[] m_child;
    }

    public string m_stageName;
    public ResultBlockInfo[] m_resultBlockInfo;
}
