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
    void OnCollisionGroundEnter();

    /// <summary>
    /// 地面から離れた
    /// </summary>
    void OnCollisionGroundExit();

    /// <summary>
    /// 壁に衝突
    /// </summary>
    void OnCollisionWallEnter();

    /// <summary>
    /// 壁から離れた
    /// </summary>
    void OnCollisionWallExit();
}
