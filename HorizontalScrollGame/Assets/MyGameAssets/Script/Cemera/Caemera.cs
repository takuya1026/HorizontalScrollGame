using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caemera : MonoBehaviour
{
    [SerializeField]                               private GameObject  m_followObject       = default;  //視点となるオブジェクトs
    [SerializeField,Header("視点からの距離")]      public float        m_cameraDistance     = 2.5f;     //視点からのカメラまでの距離
    [SerializeField,Header("注目点からの高さ")]    public float        m_currentCameraDipth = 1f;       //現在のカメラの高さ
    [SerializeField, Header("移動開始の遊び")]     public float        m_cameraPlayDistance = 0.3f;     //視点からカメラまでの距離の遊び
    [SerializeField, Header("離れる時の速度")]     public float        m_leaveSpeed         = 20f;      //離れる時の速度

    private Vector3      m_lookPos = Vector3.zero;   //実際にカメラを向ける座標
    private  float       m_lookPlayerDistance = 0.3f; //視点の遊び
    private float        m_followSpeed = 4f;          //追いかける時の速度
    private void Awake()
    {
        //XとYは視点と同じ
        float vecZ = m_followObject.transform.position.z - m_cameraDistance;
        Vector3 cameraPos = new Vector3(transform.position.x, transform.position.y, vecZ);
        transform.position = cameraPos;
    }

    /// <summary>
    /// 注目点の更新
    /// </summary>
    void UpdateLookPos()
    {
        //目標の視点と現在の視点の距離を求める
        Vector3 vec = m_followObject.transform.position - m_lookPos;
        float distance = vec.magnitude;

        if (distance > m_lookPlayerDistance)
        {
            //遊びの距離を超えていたら目標の視点に近づける
            float moveDistance = (distance - m_lookPlayerDistance) * (Time.deltaTime * m_followSpeed);
            m_lookPos += vec.normalized * moveDistance;
        }
    }

    /// <summary>
    /// カメラの位置を更新
    /// </summary>
    private void UpdateCameraPos()
    {
        //X平面におけるカメラの視点の距離を取得する
        Vector3 xy_vec = m_followObject.transform.position - transform.position;
        xy_vec.z = transform.position.z;
        float distance = xy_vec.magnitude;

        //カメラの移動距離を求める
        float move_distance = 0f;

        if (distance > m_cameraDistance + m_cameraPlayDistance)
        {
            //カメラが遊びを超えて離れたら追いかける
            move_distance = distance - (m_cameraDistance + m_cameraPlayDistance);
            move_distance *= Time.deltaTime * m_leaveSpeed;
        }
        
        else if (distance < m_cameraDistance - m_cameraPlayDistance)
        {
            //カメラが遊びを超えて近づいたら離れる
            move_distance = distance - (m_cameraDistance - m_cameraPlayDistance);
            move_distance *= Time.deltaTime * m_leaveSpeed;
        }

        //新しいカメラの位置を求める
        Vector3 camera_Pos = transform.position + (xy_vec.normalized * move_distance);
        camera_Pos.y = m_lookPos.y + m_currentCameraDipth;
        Vector3 newPos = new Vector3(camera_Pos.x, camera_Pos.y, transform.position.z);

        transform.position = newPos;
    }

    private void FixedUpdate()
    {
        if (m_followObject == null) return;

        UpdateLookPos();
        UpdateCameraPos();
    }
}
