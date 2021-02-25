using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionChecker : MonoBehaviour
{
    /// <summary>
    /// 敵の種類ごとのパラメータ
    /// </summary>
    [SerializeField]
    protected EnemyTypeParameter m_typeParameter = null;

    /// <summary>
    /// レイ
    /// </summary>
    private Ray m_ray = new Ray();

    /// <summary>
    /// 地面に接地した
    /// </summary>
    private Action m_onCollisionGround;

    /// <summary>
    /// 壁に接触した
    /// </summary>
    private Action m_onCollisionWall;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize(Action onCollisionGround = null, Action onCollisionWall = null)
    {
        m_onCollisionGround = onCollisionGround;
        m_onCollisionWall = onCollisionWall;
    }

    /// <summary>
    /// 接地しているか
    /// </summary>
    public bool IsGround()
    {
        m_ray.origin = transform.position;
        m_ray.direction = Vector3.down;
        var sphereCastParam = m_typeParameter.m_SphereCastParameter;

        // 判定
        if (Physics.SphereCast(m_ray, sphereCastParam.m_radius, sphereCastParam.m_distance))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 衝突
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        // 地面
        if (IsGround())
        {
            m_onCollisionGround?.Invoke();
        }
        // 壁（天井は考慮しない）
        else
        {
            m_onCollisionWall?.Invoke();
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// ギズモ表示
    /// </summary>
    private void OnDrawGizmos()
    {
        IsGround();

        // スフィアキャストを表示
        var sphereCastParam = m_typeParameter.m_SphereCastParameter;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(m_ray.origin, m_ray.direction * sphereCastParam.m_distance);
        Gizmos.DrawWireSphere(m_ray.GetPoint(sphereCastParam.m_distance), sphereCastParam.m_radius);
        Gizmos.color = Color.white;
    }
#endif
}
