
//============================================================
// @file GimmickBase
// @brief ギミックベース
// @autor ochi takuya
//============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ギミックベース
/// </summary>
public class GimmickBase
{
    // protected member
    protected string m_name;
    protected GimmickType m_gimmickType;
    protected GameObject m_object;

    /// <summary>
    /// 名前
    /// </summary>
    public string m_Name { get { return m_name; } protected set { m_name = value; } }

    /// <summary>
    /// ギミックタイプ
    /// </summary>
    public GimmickType m_GimmickType { get { return m_gimmickType; } protected set { m_gimmickType = value; } }

    /// <summary>
    /// オブジェクト
    /// </summary>
    public GameObject m_Object { get { return m_object; } protected set { m_object = value; } }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public GimmickBase() { /* 処置なし */ }

    /// <summary>
    /// デストラクタ
    /// </summary>
    ~GimmickBase() { /* 処置なし */ }

    /// <summary>
    /// 起動時処理
    /// </summary>
    public virtual void Awake() { /* 処置なし */ }

    /// <summary>
    /// 開始時処理
    /// </summary>
    public virtual void Start() { /* 処置なし */ }

    /// <summary>
    /// 更新処理
    /// </summary>
    public virtual void Update() { /* 処置なし */ }
}
