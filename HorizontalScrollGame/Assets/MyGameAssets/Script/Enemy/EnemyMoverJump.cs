using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMoverJump : EnemyMover
{
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
        float radAngle = 60 * Mathf.Deg2Rad;
        Vector3 vec = new Vector3(Mathf.Cos(radAngle), Mathf.Sin(radAngle), 0);
        m_target.AddForce(vec * 300);

        // 地面から離れるまで
        while (IsGround()) { yield return null; }

        // 地面に着地するまで
        while (!IsGround()) { yield return null; }

        onEnd?.Invoke();
    }
}
