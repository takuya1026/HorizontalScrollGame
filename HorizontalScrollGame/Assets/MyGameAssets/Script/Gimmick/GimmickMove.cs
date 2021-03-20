
//============================================================
// @file GimmickMove
// @brief 移動系ギミック
// @autor ochi takuya
//============================================================

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移動系ギミック
/// </summary>
public class GimmickMove : GimmickBase
{
    [SerializeField, Tooltip("移動種類")]
    private MoveType m_moveType = default;

    [SerializeField, Tooltip("移動の終点位置")]
    private float m_targetValue = default;

    [SerializeField, Tooltip("移動速度")]
    private float m_speed = default;

    [SerializeField, Tooltip("クローン時間")]
    private float m_cloneInterval = default;
    private float m_cloneTime = default;
    public float m_CloneTime { set { m_cloneTime = value; } }

    private Vector3 m_startPositon = default;
    private Vector3 m_targetPositon = default;
    private bool m_isWrapBack = default;
    private List<GameObject> m_cloneList = default;
    private List<int> m_removeIndexList = default;

    /// <summary>
    /// 移動タイプ
    /// </summary>
    private enum MoveType
    {
        NONE,
        UP_AND_DOWN,
        LEFT_AND_RIGHT,
        UP_INFINITE,
        DOWN_INFINITE,
        FALL,
        ROTATION,
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public GimmickMove(string name)
    {
        m_Name = name;
    }

    /// <summary>
    /// デストラクタ
    /// </summary>
    ~GimmickMove()
    {

    }

    private void Start()
    {
        IndividualStart();
    }

    private void Update()
    {
        IndividualUpdate();
    }

    /// <summary>
    /// 起動時処理
    /// </summary>
    public override void IndividualAwake()
    {

    }

    /// <summary>
    /// 開始時処理
    /// </summary>
    public override void IndividualStart()
    {
        m_startPositon = transform.position;

        switch (m_moveType)
        {
            case MoveType.UP_AND_DOWN:
                m_targetPositon = new Vector3(transform.position.x, transform.position.y + m_targetValue, transform.position.z);
                break;
            case MoveType.LEFT_AND_RIGHT:
                m_targetPositon = new Vector3(transform.position.x + m_targetValue, transform.position.y, transform.position.z);
                break;
            case MoveType.UP_INFINITE:
                if (m_cloneList == default)
                {
                    m_cloneList = new List<GameObject>();
                }
                if (m_removeIndexList == default)
                {
                    m_removeIndexList = new List<int>();
                }
                m_cloneList.Clear();
                m_cloneTime = m_cloneInterval;
                m_targetPositon = new Vector3(transform.position.x, transform.position.y + m_targetValue, transform.position.z);
                break;
            case MoveType.DOWN_INFINITE:
                if (m_cloneList == default)
                {
                    m_cloneList = new List<GameObject>();
                }
                if (m_removeIndexList == default)
                {
                    m_removeIndexList = new List<int>();
                }
                m_cloneList.Clear();
                m_cloneTime = m_cloneInterval;
                m_targetPositon = new Vector3(transform.position.x + m_targetValue, transform.position.y, transform.position.z);
                break;
            case MoveType.FALL:
                break;
            case MoveType.ROTATION:
                break;
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    public override void IndividualUpdate()
    {
        move();
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void move()
    {
        switch (m_moveType)
        {
            case MoveType.UP_AND_DOWN:
                moveUpAndDown();
                break;
            case MoveType.LEFT_AND_RIGHT:
                moveLeftAndRight();
                break;
            case MoveType.UP_INFINITE:
                moveUpInfinite();
                break;
            case MoveType.DOWN_INFINITE:
                break;
            case MoveType.FALL:
                break;
            case MoveType.ROTATION:
                break;
        }
    }

    /// <summary>
    /// 上下に移動
    /// </summary>
    private void moveUpAndDown()
    {
        if (transform.position.y == m_targetPositon.y)
        {
            m_isWrapBack = !m_isWrapBack;
            m_targetPositon.y = (m_isWrapBack) ? m_startPositon.y : transform.position.y + m_targetValue;
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, m_targetPositon, m_speed * Time.deltaTime);
    }

    /// <summary>
    /// 左右に移動
    /// </summary>
    private void moveLeftAndRight()
    {
        if (transform.position.x == m_targetPositon.x)
        {
            m_isWrapBack = !m_isWrapBack;
            m_targetPositon.x = (m_isWrapBack) ? m_startPositon.x : transform.position.x + m_targetValue;
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, m_targetPositon, m_speed * Time.deltaTime);
    }

    /// <summary>
    /// 上に無限移動
    /// </summary>
    private void moveUpInfinite()
    {
        if (m_cloneTime <= 0.0f)
        {
            m_cloneTime = m_cloneInterval;

            GameObject clone = GameObject.CreatePrimitive(PrimitiveType.Cube);
            clone.transform.parent = gameObject.transform;
            clone.transform.position = gameObject.transform.position;
            clone.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            clone.transform.rotation = gameObject.transform.rotation;

            MeshRenderer objectMeshRenderer = gameObject.GetComponent<MeshRenderer>();
            MeshRenderer cloneMeshRenderer = clone.GetComponent<MeshRenderer>();

            cloneMeshRenderer = objectMeshRenderer;

            m_cloneList.Add(clone);
        }

        for (int i = 0, length = m_cloneList.Count; i < length; ++i)
        {
            m_cloneList[i].transform.position = Vector3.MoveTowards(m_cloneList[i].transform.position, m_targetPositon, m_speed * Time.deltaTime);

            if (m_cloneList[i].transform.position.y == m_targetPositon.y)
            {
                Destroy(m_cloneList[i]);
                m_cloneList.RemoveAt(i);
                length--;
            }
        }

        for (int i = 0, length = m_removeIndexList.Count; i < length; ++i)
        {
            m_cloneList.RemoveAt(m_removeIndexList[i]);
        }

        m_removeIndexList.Clear();
        m_cloneTime--;
    }
}
