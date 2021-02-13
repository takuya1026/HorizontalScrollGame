
//============================================================
// @file PlacementPrefab
// @brief プレハブの配置を指定フォルダ内にする
// @autor ochi.takuya
//============================================================

using System;
using UnityEngine;

/// <summary>
/// プレハブの配置を指定フォルダ内にする
/// </summary>
namespace Hexat.Editor
{
    [ExecuteInEditMode]
    public class PrefabPlacementFolder : MonoBehaviour
    {
        [SerializeField]
        private bool m_isEnableParentSearchByID = false;
        public bool m_IsEnableParentSearchByID { get { return m_isEnableParentSearchByID; } set { m_isEnableParentSearchByID = value; } }

        [SerializeField]
        private string m_parentName = "";
        public string m_ParentName { get { return m_parentName; } set { m_parentName = value; } }

        [SerializeField]
        private int m_parentId = 0;
        public int m_ParentId { get { return m_parentId; } set { m_parentId = value; } }

        private readonly int MAX_PARENT_ID = 999;

        /// <summary>
        /// シーン配置時
        /// </summary>
        private void Awake()
        {
            if (gameObject != null)
            {
                GameObject parentObject = null;
                if (m_isEnableParentSearchByID)
                {
                    if (m_parentId >= MAX_PARENT_ID)
                    {
                        m_parentId = MAX_PARENT_ID;
                    }

                    parentObject = GameObject.Find((gameObject.name + "_Element_" + m_parentId.ToString("D3")));
                    if (parentObject == null)
                    {
                        return;
                    }
                }
                else
                {
                    parentObject = GameObject.Find(m_parentName);
                    if (parentObject == null)
                    {
                        return;
                    }
                }

                gameObject.transform.parent = parentObject.transform;
            }
        }
    }
}
