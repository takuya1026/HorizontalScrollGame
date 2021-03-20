
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
        /// <summary>
        /// 開始
        /// 親オブジェクトでプレハブ化を解除してる場合、処理しない
        /// </summary>
        private void Awake()
        {
            if (gameObject == null)
            {
                return;
            }

            // プレハブではない
            if (string.IsNullOrEmpty(PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(gameObject)))
            {
                return;
            }

            PrefabUtility.UnpackPrefabInstance(gameObject, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
        }
    }
}
