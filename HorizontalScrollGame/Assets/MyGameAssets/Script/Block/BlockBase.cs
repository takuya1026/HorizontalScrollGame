
//============================================================
// @file BlockBase
// @brief ブロックベース
// @autor ochi takuya
//============================================================

using UnityEngine;
using DG.Tweening;

/// <summary>
/// ブロックベース
/// </summary>
public class BlockBase : MonoBehaviour
{
    protected Vector3 m_postion = default;
    protected Quaternion m_rotation = default;
    protected Vector3 m_scale = default;
    protected Renderer m_renderer = default;
    protected Material m_material = default;
    protected Texture m_texture = default;
    protected bool m_isAction = false;
    protected bool m_isRunAction = false;
    protected bool m_isEndAction = false;

    [SerializeField, Tooltip("ブロックタイプ")]
    protected EnumBlockType m_enumBlockType = default;

    [SerializeField, Tooltip("アイテムID")]
    protected int m_itemId = default;

    [SerializeField, Tooltip("個数")]
    protected int m_quantity = 1;

    [SerializeField, Tooltip("テスクチャ")]
    protected string m_texName = default;

    public Vector3          m_Postion   => gameObject.transform.position;
    public Quaternion       m_Rotation  => gameObject.transform.rotation;
    public Vector3          m_Scale     => gameObject.transform.localScale;

    public EnumBlockType    m_EnumBlockType {   get { return m_enumBlockType; }   set { m_enumBlockType = value; }  }
    public string           m_TexName       {   get { return m_texName; }         set { m_texName = value; }        }

    public int m_ItemId
    {
        get
        {
            if (! IsItemAndCoinBlock())
            {
                m_itemId = 0;
            }
            return m_itemId;
        }
        set
        {
            m_itemId = value;
        }

    }

    public int m_Quantity
    {
        get
        {
            if (! IsItemAndCoinBlock())
            {
                m_quantity = 0;
            }
            return m_quantity;
        }
        set
        {
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

    /// <summary>
    /// 初期化
    /// </summary>
    public virtual void Init()
    {
        // 処理なし
    }

    /// <summary>
    /// 更新
    /// </summary>
    public virtual void Execute()
    {
        // 処理なし
    }

    /// <summary>
    /// ヒット
    /// </summary>
    protected virtual void hit()
    {
        // 処理なし
    }

    /// <summary>
    /// アクション
    /// </summary>
    protected virtual void action()
    {
        if (m_isAction && !m_isRunAction && !m_isEndAction)
        {
            transform.DOJump(transform.position, 0.4f, 1, 0.2f)
                .SetEase(Ease.Linear)
                .OnComplete(() => {
                    m_isAction = false;
                    m_isRunAction = false;
                    m_isEndAction = true;
                });

            m_isRunAction = true;
        }
    }

    /// <summary>
    /// 終了アクションを無効にする
    /// </summary>
    protected virtual void disableEndAction()
    {
        m_isEndAction = false;
    }

    /// <summary>
    /// テスクチャ変更
    /// </summary>
    /// <param name="path">path from resource</param>
    /// <param name="textureName">texture name</param>
    protected virtual void changeTexture(string path = "", string textureName = "")
    {
        if (path == "")
        {
            path = "Assets/Resources/Texture/Stage/";
        }

        if (textureName == "")
        {
            textureName = "MapTexture05.png";
        }

        m_texture = Utility.Read.ReadTexture((path + textureName));
        if (m_texture == null)
        {
            Debug.Log("ERROR: 【 " + path + textureName + " 】not texture. (BlockBase#_chabgeTexture)");
            return;
        }

        if (m_material == null)
        {
            m_material = GetComponent<Renderer>().material;
        }

        m_material.SetTexture("_MainTex", m_texture);
    }

    /// <summary>
    /// 破壊
    /// </summary>
    protected virtual void destruction()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
