
//============================================================
// @file BlockNormal
// @brief 通常ブロック
// @autor ochi takuya
//============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 通常ブロック
/// </summary>
public class BlockNormal : BlockBase
{
    /// <summary>
    /// 初期化
    /// </summary>
    public override void Init()
    {
        // 処理なし
    }

    /// <summary>
    /// 更新
    /// </summary>
    public override void Execute()
    {
        hit();
        action();
        disableEndAction();
    }

    /// <summary>
    /// ヒット
    /// </summary>
    protected override void hit()
    {
        bool isHit = Input.GetKeyDown(KeyCode.A);
        if (isHit)
        {
            m_isAction = true;
        }
    }
}
