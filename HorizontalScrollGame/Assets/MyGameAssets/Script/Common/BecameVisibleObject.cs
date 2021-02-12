
//============================================================
// @file BecameVisibleObject
// @brief オブジェクトのカメラ内判定
// @autor ochi takuya
//============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトのカメラ内判定
/// </summary>
public class BecameVisibleObject : MonoBehaviour
{
    private Collider m_collider = null;

    /// <summary>
    /// 開始
    /// </summary>
    private void Awake()
    {
        m_collider = GetComponent<Collider>();
    }

    /// <summary>
    /// カメラ内に入った時に、1度呼ばれる
    /// </summary>
    private void OnBecameVisible()
    {
        if (m_collider == null)
        {
            return;
        }

        m_collider.enabled = true;
    }

    /// <summary>
    /// カメラ外に出た時に、1度呼ばれる
    /// </summary>
    private void OnBecameInvisible()
    {
        if (m_collider == null)
        {
            return;
        }

        m_collider.enabled = false;
    }
}
