using UnityEngine;

public class EnemyMoverJump : EnemyMover<EnemyJumpParameter>
{
    /// <summary>
    /// 開始
    /// </summary>
    public override void Execute()
    {
        Jump();
    }

    /// <summary>
    /// 停止
    /// </summary>
    public override void Stop()
    {
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    private void Jump()
    {
        Vector3 jumpAngleVector = Utility.Math.AngleToVector(m_moveParameter.m_JumpAngle);
        m_target.AddForce(jumpAngleVector * m_moveParameter.m_JumpPower, ForceMode.Impulse);
    }

    protected override void OnCollisionGround()
    {
        // 物理を一度停止して
        m_target.Sleep();
        Jump();
    }

    protected override void OnCollisionWall()
    {
        m_target.velocity *= -1;
        m_target.Sleep();
        Jump();
    }
}
