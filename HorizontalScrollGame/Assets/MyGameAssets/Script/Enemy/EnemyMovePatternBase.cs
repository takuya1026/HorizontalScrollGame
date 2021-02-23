using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovePatternBase
{
    /// <summary>
    /// 制御対象のRigidbody
    /// </summary>
    protected Rigidbody m_target;

    /// <summary>
    /// 
    /// </summary>
    protected EnemyTypeParameter.SphereCastParameter m_sphereCastParameter;

    

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize(Rigidbody target, EnemyTypeParameter.SphereCastParameter sphereCastParameter)
    {
        m_target = target;
        m_sphereCastParameter = sphereCastParameter;
    }
}
