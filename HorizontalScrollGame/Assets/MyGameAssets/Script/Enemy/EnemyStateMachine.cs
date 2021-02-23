using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    protected enum State
    {
        WANDERING,  // 徘徊中
        FALLING,    // 落下中
        DEAD,       // おなくなりー
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private EnemyMover m_mover;

    /// <summary>
    /// 現在のステート
    /// </summary>
    protected State m_currentState = State.WANDERING;

    /// <summary>
    /// 飛行中
    /// </summary>
    protected bool m_isFlying = false;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize(EnemyMover mover,bool isFlying = false)
    {
        m_mover = mover;
        m_isFlying = isFlying;
        mover.Execute();
    }
}
