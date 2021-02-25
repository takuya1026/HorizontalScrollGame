using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("敵の移動パラメータ"), SerializeField]
    private EnemyMoveParameter m_moveParameter = null;
    
    /// <summary>
    /// 移動対象のRigidbody
    /// </summary>
    [Header("その他コンポーネント")]
    [SerializeField]
    private Rigidbody m_target = null;

    /// <summary>
    /// 敵の衝突判定
    /// </summary>
    [SerializeField]
    private EnemyCollisionChecker m_collisionChecker = null;

    /// <summary>
    /// 敵のジャンプ制御
    /// </summary>
    private IEnemyMoverExecuter m_enemyMover;

    /// <summary>
    /// ステートマシン
    /// </summary>
    private EnemyStateMachine m_stateMachine = new EnemyStateMachine();

    private void Start()
    {
        switch (m_moveParameter)
        {
            case EnemyJumpParameter jump:
                var moverJump = new EnemyMoverJump();
                moverJump.Initialize(m_target, m_collisionChecker, jump);
                m_enemyMover = moverJump;
                break;
        }

        m_stateMachine.Initialize(m_enemyMover);
    }
}
