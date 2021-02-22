
//============================================================
// @file BlockInfo
// @brief ブロックの情報
// @autor ochi takuya
//============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ブロックの情報
/// </summary>
public class BlockInfo : MonoBehaviour
{
    private Vector3 m_postion;
    private Quaternion m_rotation;
    private Vector3 m_scale;

    [SerializeField, Tooltip("ブロックタイプ")]
    private EnumBlockType m_enumBlockType = EnumBlockType.NONE;

    [SerializeField, Tooltip("アイテム")]
    private int m_itemId;

    [SerializeField, Tooltip("個数")]
    private int m_quantity;

    public EnumBlockType m_EnumBlockType { get { return m_enumBlockType; } set { m_enumBlockType = value; } }

    public Vector3 m_Postion {
        get {
            m_postion = gameObject.transform.position;
            return m_postion;
        }
        set {
            m_postion = value;
        }
    }

    public Quaternion m_Rotation
    {
        get
        {
            m_rotation = gameObject.transform.rotation;
            return m_rotation;
        }
        set
        {
            m_rotation = value;
        }
    }

    public Vector3 m_Scale
    {
        get
        {
            m_scale = gameObject.transform.localScale;
            return m_scale;
        }
        set
        {
            m_scale = value;
        }
    }

    public int m_ItemId {
        get {

            if (! IsItemAndCoinBlock())
            {
                m_itemId = 0;
            }

            return m_itemId;
        }
        set {
            m_itemId = value;
        }

    }

    public int m_Quantity {
        get {

            if (!IsItemAndCoinBlock())
            {
                m_quantity = 0;
            }

            return m_quantity;
        }
        set {
            m_quantity = value;
        }
    }

    /// <summary>
    /// アイテムとコインのブロック
    /// </summary>
    /// <returns>アイテムとコインの場合：true</returns>
    public bool IsItemAndCoinBlock()
    {
        if (m_enumBlockType == EnumBlockType.COIN_BLOCK
         || m_enumBlockType == EnumBlockType.ITEM_BLOCK)
        {
            return true;
        }
        return false;
    }

}
