
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
        [SerializeField, Tooltip("親オブジェクトの名前")]
        private string m_parentName = "";

        private void Awake()
        {
            if (gameObject != null)
            {
                GameObject parentObject = GameObject.Find(m_parentName);
                if (parentObject == null)
                {
                    return;
                }

                gameObject.transform.parent = parentObject.transform;

            }
        }
    }
}
