
//============================================================
// @file BlockDrop
// @brief ドロップ系ブロック
// @autor ochi takuya
//============================================================

using UnityEngine;
using DG.Tweening;

/// <summary>
/// ドロップ系ブロック
/// </summary>
public class BlockDrop : BlockBase
{
    private bool m_isDrop = false;
    private bool m_isLastDrop = false;

    [SerializeField, Tooltip("仮：アイテムが出来たら消す")]
    private GameObject tmpObj;

    /// <summary>
    /// 初期化
    /// </summary>
    public override void Init()
    {
        m_isDrop = false;
        m_isLastDrop = false;
    }

    /// <summary>
    /// 更新
    /// </summary>
    public override void Execute()
    {
        if (m_isLastDrop)
        {
            return;
        }

        hit();
        action();
        dorp();
        lastDrop();
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
            m_isDrop = true;
        }
    }

    /// <summary>
    /// ドロップ
    /// </summary>
    private void dorp()
    {
        if (m_isDrop)
        {
            createItem();

            m_quantity--;
            m_isDrop = false;
        }
    }

    /// <summary>
    /// 最後のドロップ
    /// </summary>
    private void lastDrop()
    {
        bool isChangeTexture = m_quantity <= 0;
        if (isChangeTexture)
        {
            changeTexture();
            m_isLastDrop = true;
        }
    }

    /// <summary>
    /// アイテムの生成
    /// </summary>
    private void createItem()
    {
        // TODO; アイテムIDから生成できるようにする。

        GameObject newObject = Instantiate(tmpObj, transform.position, Quaternion.identity);
        if (m_enumBlockType == EnumBlockType.COIN_BLOCK)
        {
            newObject.transform.DOMove(new Vector3(transform.position.x, (transform.position.y + 2.0f), transform.position.z), 0.5f);
        }
        else if (m_enumBlockType == EnumBlockType.ITEM_BLOCK)
        {
            newObject.transform.DOMove(new Vector3(transform.position.x, (transform.position.y + 1.0f), transform.position.z), 0.5f);
        }
    }
}
