using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 敵の種類ごとのパラメータ
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/EnemyTypeParameter")]
public class EnemyTypeParameter : ScriptableObject
{
    [Serializable]
    public class SphereCastParameter
    {
        /// <summary>
        /// 距離
        /// </summary>
        public float m_distance;

        /// <summary>
        /// 半径
        /// </summary>
        public float m_radius;
    }

    /// <summary>
    /// レイ関連のパラメータ
    /// </summary>
    [SerializeField]
    private SphereCastParameter m_sphereCastParameter = null;
    public SphereCastParameter m_SphereCastParameter => m_sphereCastParameter;
}
