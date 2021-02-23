using UnityEngine;

public abstract class EnemyMover : MonoBehaviour
{
    /// <summary>
    /// 移動対象のRigidbody
    /// </summary>
    [SerializeField]
    protected Rigidbody m_target = null;

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
    /// 開始
    /// </summary>
    abstract public void Execute();
    
    /// <summary>
    /// 終了
    /// </summary>
    abstract public void End();

    /// <summary>
    /// 接地しているか
    /// </summary>
    protected bool IsGround()
    {
        m_ray.origin = m_target.transform.position;
        m_ray.direction = Vector3.down;
        var sphereCastParam = m_typeParameter.m_SphereCastParameter;

        // 判定
        if (Physics.SphereCast(m_ray, sphereCastParam.m_radius, sphereCastParam.m_distance))
        {
            return true;
        }
        return false;
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
