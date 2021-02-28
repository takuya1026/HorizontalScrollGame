using UnityEngine;

public abstract class EnemyMover<Parameter> : IEnemyMover where Parameter : EnemyMoveParameter
{
    /// <summary>
    /// 移動対象のRigidbody
    /// </summary>
    protected Rigidbody m_target;

    /// <summary>
    /// 敵の衝突判定
    /// </summary>
    protected EnemyCollisionChecker m_collisionChecker;

    /// <summary>
    /// 敵の移動パラメータ
    /// </summary>
    protected Parameter m_moveParameter;

    /// <summary>
    /// 初期化
    /// </summary>
    public virtual void Initialize(Rigidbody target, Parameter moveParameter)
    {
        m_target = target;
        m_moveParameter = moveParameter;
    }

    /// <summary>
    /// 開始
    /// </summary>
    public abstract void Execute();
    
    /// <summary>
    /// 停止
    /// </summary>
    public abstract void Stop();

    /// <summary>
    /// 地面に接地した
    /// </summary>
    public abstract void OnCollisionGround();

    /// <summary>
    /// 壁に接触した
    /// </summary>
    public abstract void OnCollisionWall();
}
