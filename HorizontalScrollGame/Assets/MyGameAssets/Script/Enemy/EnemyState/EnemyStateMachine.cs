﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : IEnemyCollisionCallbacker
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
    private IEnemyMover m_enemyMover;

    /// <summary>
    /// 敵の飛行中の移動制御
    /// </summary>
    private IEnemyMover m_enemyFlyingMover;

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
    public void Initialize(IEnemyMover enemyMover, IEnemyMover enemyFlyingMover = null, bool isFlying = false)
    {
        m_enemyMover = enemyMover;
        m_enemyFlyingMover = enemyFlyingMover;
        m_isFlying = isFlying;
    }

    /// <summary>
    /// 地面に接地
    /// </summary>
    public void OnCollisionGroundEnter()
    {
        // 落下終了（地面に着地した）
        if (m_currentState == State.FALLING)
        {
            OnEndFall();
            m_currentState = State.WANDERING;
        }
    }

    /// <summary>
    /// 地面から離れた
    /// </summary>
    public void OnCollisionGroundExit()
    {

    }

    /// <summary>
    /// 壁に衝突
    /// </summary>
    public void OnCollisionWallEnter()
    {

    }

    /// <summary>
    /// 壁から離れた
    /// </summary>
    public void OnCollisionWallExit()
    {
        
    }

    

    /// <summary>
    /// 落下が開始した
    /// </summary>
    private void OnStartFall()
    {
        
    }

    /// <summary>
    /// 落下が終了した
    /// </summary>
    private void OnEndFall()
    {

    }
}
