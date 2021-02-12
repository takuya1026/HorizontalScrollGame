
//============================================================
// @file UnpackPrefub
// @brief Prefabとしての接続を切る
// @autor ochi.takuya
//============================================================

using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Prefabとしての接続を切る
/// </summary>
namespace Hexat.Editor
{
    [ExecuteInEditMode]
    public class UnpackPrefub : MonoBehaviour
    {
        private bool m_isAlreadyUnpackPrefab = false;
        private GameObject m_parentObject = null;
        private UnpackPrefub m_parentUnpackPrefub = null;

        /// <summary>
        /// プレハブ状態を解除済みか
        /// </summary>
        public bool m_IsAlreadyUnpackPrefab { get { return m_isAlreadyUnpackPrefab; } set { m_isAlreadyUnpackPrefab = value; } }

        /// <summary>
        /// 開始
        /// 親オブジェクトでプレハブ化を解除してる場合、処理しない
        /// </summary>
        private void Awake()
        {
            if (gameObject != null)
            {
                if (gameObject.transform.parent != null && gameObject.transform.parent.gameObject != null)
                {
                    m_parentObject = gameObject.transform.parent.gameObject;
                    m_parentUnpackPrefub = m_parentObject.GetComponent<UnpackPrefub>();
                    m_IsAlreadyUnpackPrefab = m_parentUnpackPrefub.m_IsAlreadyUnpackPrefab;

                    if (m_IsAlreadyUnpackPrefab)
                    {
                        return;
                    }
                }

                PrefabUtility.UnpackPrefabInstance(gameObject, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
                m_IsAlreadyUnpackPrefab = true;
            }
        }
    }
}
