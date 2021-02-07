
//============================================================
// @file TestCameraMove
// @brief テストコード：カメラ移動
// @autor ochi takuya
//============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// テストコード：カメラ移動
/// </summary>
public class TestCameraMove : MonoBehaviour
{
    [SerializeField, Tooltip("カメラ")]
    private Camera m_camera = null;

    [SerializeField, Range(0.1f, 10.0f), Tooltip("カメラの移動量")]
    private float m_speed = 0.0f;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        m_camera.gameObject.transform.Translate(new Vector3((Time.deltaTime * m_speed), 0.0f, 0.0f));
    }
}
