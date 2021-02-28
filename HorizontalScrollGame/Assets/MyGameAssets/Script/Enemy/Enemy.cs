using System;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// エネミーの管理クラス
/// </summary>
public class Enemy : MonoBehaviour
{
    /// <summary>
    /// 移動パラメータ
    /// </summary>
    [Header("敵の移動パラメータ")]
    [SerializeField]
    private EnemyMoveParameter m_moveParameter = null;
    
    /// <summary>
    /// 飛行時の移動パラメータ
    /// </summary>
    [SerializeField]
    private EnemyMoveParameter m_flyingMoveParameter = null;
    
    /// <summary>
    /// 飛ぶかどうか
    /// </summary>
    [SerializeField]
    private bool m_isFlying = false;


    /// <summary>
    /// 対象のRigidbody
    /// </summary>
    [Header("その他コンポーネント（プログラマー以外触らないで！）"),Space]
    [SerializeField]
    private Rigidbody m_target = null;

    /// <summary>
    /// 敵の衝突判定
    /// </summary>
    [SerializeField]
    private EnemyCollisionChecker m_collisionChecker = null;

    /// <summary>
    /// 敵の移動制御
    /// </summary>
    private IEnemyMover m_enemyMover;

    /// <summary>
    /// 敵の飛行中の移動制御
    /// </summary>
    private IEnemyMover m_enemyFlyingMover;

    /// <summary>
    /// ステートマシン
    /// </summary>
    private EnemyStateMachine m_stateMachine = new EnemyStateMachine();

    /// <summary>
    /// 開始
    /// </summary>
    private void Start()
    {
        m_collisionChecker.Initialize(OnCollisionGround, OnCollisionWall);
        m_enemyMover = InitializeMover(m_moveParameter);
        if (m_flyingMoveParameter != null)
        {
            m_enemyFlyingMover = InitializeMover(m_flyingMoveParameter);
        }
        m_stateMachine.Initialize(m_enemyMover, m_enemyFlyingMover, m_isFlying);
    }

    /// <summary>
    /// Moverクラスの初期化
    /// </summary>
    private IEnemyMover InitializeMover(EnemyMoveParameter parameter)
    {
        // ダウンキャストで型を調べて初期化
        switch (parameter)
        {
            case EnemyJumpParameter jump:
                var moverJump = new EnemyMoverJump();
                moverJump.Initialize(m_target, jump);
                return moverJump;
            default:
                throw new ArgumentException();
        }
    }

    private void OnCollisionGround()
    {
        m_stateMachine?.OnCollisionGround();
        m_enemyMover?.OnCollisionGround();
        m_enemyFlyingMover?.OnCollisionGround();
        
    }

    private void OnCollisionWall()
    {
        m_stateMachine?.OnCollisionWall();
        m_enemyMover?.OnCollisionWall();
        m_enemyFlyingMover?.OnCollisionWall();
    }
}
