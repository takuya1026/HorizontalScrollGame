
//============================================================
// @file EditCameraScroll
// @brief エディット：カメラスクロール
// @autor ochi takuya
//============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class EditCameraScroll : MonoBehaviour
{
    [SerializeField, Tooltip("カメラ")]
    private Camera m_camera = null;

    [SerializeField, Tooltip("移動速度"), Range(0.1f, 100.0f)]
    private float m_speed = 0;

    private Vector3 position;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        position = m_camera.gameObject.transform.position;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.W))
            {
                position.z += (Time.deltaTime * m_speed);
            }

            if (Input.GetKey(KeyCode.S))
            {
                position.z -= (Time.deltaTime * m_speed);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                position.y += (Time.deltaTime * m_speed);
            }

            if (Input.GetKey(KeyCode.S))
            {
                position.y -= (Time.deltaTime * m_speed);
            }

            if (Input.GetKey(KeyCode.A))
            {
                position.x -= (Time.deltaTime * m_speed);
            }

            if (Input.GetKey(KeyCode.D))
            {
                position.x += (Time.deltaTime * m_speed);
            }
        }

        m_camera.gameObject.transform.position = position;
    }
}
