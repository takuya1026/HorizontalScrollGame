using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// 移動処理
    /// </summary>
    [SerializeField]
    private EnemyMover m_enemyMover = null;

    /// <summary>
    /// ステートマシン
    /// </summary>
    EnemyStateMachine m_stateMachine = new EnemyStateMachine();

    private void Start()
    {
        m_stateMachine.Initialize(m_enemyMover);
    }
}
