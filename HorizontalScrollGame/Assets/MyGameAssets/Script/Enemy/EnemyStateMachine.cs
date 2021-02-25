using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    private enum State
    {
        WANDERING,  // 徘徊中
        FALLING,    // 落下中
        DEAD,       // おなくなりー
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private IEnemyMoverExecuter m_enemyMover;

    /// <summary>
    /// 現在のステート
    /// </summary>
    private State m_currentState = State.WANDERING;

    /// <summary>
    /// 飛行中
    /// </summary>
    private bool m_isFlying = false;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize(IEnemyMoverExecuter enemyMover, bool isFlying = false)
    {
        m_enemyMover = enemyMover;
        m_isFlying = isFlying;
        m_enemyMover.Execute();
    }
}
