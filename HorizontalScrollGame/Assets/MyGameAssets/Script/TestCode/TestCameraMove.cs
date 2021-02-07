using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraMove : MonoBehaviour
{
    [SerializeField, Tooltip("カメラ")]
    private Camera m_camera = null;

    [SerializeField, Range(0.0f, 10.0f), Tooltip("カメラの移動量")]
    private float m_speed = 0.0f;

    void Update()
    {
        m_camera.gameObject.transform.Translate(new Vector3(m_speed, 0.0f, 0.0f));
    }
}
