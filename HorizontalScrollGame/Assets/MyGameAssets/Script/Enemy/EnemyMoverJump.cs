using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMoverJump : EnemyMover
{
    /// <summary>
    /// ジャンプパラメータ
    /// </summary>
    [SerializeField]
    private EnemyMoveJumpParameter m_jumpParameter = null;

    /// <summary>
    /// ジャンプコルーチン
    /// </summary>
    Coroutine m_jumpCoroutine;

    /// <summary>
    /// 開始
    /// </summary>
    public override void Execute()
    {
        m_jumpCoroutine = StartCoroutine(Jump(() =>
        {
            StopCoroutine(m_jumpCoroutine);
            m_jumpCoroutine = null;
            Execute();
            Debug.Log("");
        }));
    }

    /// <summary>
    /// 終了
    /// </summary>
    public override void End()
    {

    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    IEnumerator Jump(Action onEnd = null)
    {
        Vector3 jumpAngleVector = Utility.Math.AngleToVector(m_jumpParameter.m_JumpAngle);
        m_target.AddForce(jumpAngleVector * m_jumpParameter.m_JumpPower);

        // 地面から離れるまで
        while (IsGround()) { yield return null; }

        // 地面に着地するまで
        while (!IsGround()) { yield return null; }

        onEnd?.Invoke();
    }
}
