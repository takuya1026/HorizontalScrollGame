public interface IEnemyMover : IEnemyMoverExecuter, IEnemyCollisionCallbacker
{

}

public interface IEnemyMoverExecuter
{
    /// <summary>
    /// 実行
    /// </summary>
    void Execute();

    /// <summary>
    /// 停止
    /// </summary>
    void Stop();
}

public interface IEnemyCollisionCallbacker
{
    /// <summary>
    /// 地面に接地
    /// </summary>
    void OnCollisionGround();

    /// <summary>
    /// 壁に衝突
    /// </summary>
    void OnCollisionWall();
}
