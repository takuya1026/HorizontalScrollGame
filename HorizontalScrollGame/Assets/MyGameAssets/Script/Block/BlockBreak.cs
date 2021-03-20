
//============================================================
// @file BlockBreak
// @brief 破壊系ブロック
// @autor ochi takuya
//============================================================

using UnityEngine;

/// <summary>
/// 破壊系ブロック
/// </summary>
public class BlockBreak : BlockBase
{
    private bool m_isBreak = false;

    /// <summary>
    /// 初期化
    /// </summary>
    public override void Init()
    {
        m_isBreak = false;
    }

    /// <summary>
    /// 更新
    /// </summary>
    public override void Execute()
    {
        if (m_isBreak)
        {
            return;
        }

        hit();
        action();
        erase();
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

    /// <summary>
    /// 削除
    /// </summary>
    private void erase()
    {
        if (m_isEndAction)
        {
            destruction();
            m_isBreak = true;
        }
    }
}
