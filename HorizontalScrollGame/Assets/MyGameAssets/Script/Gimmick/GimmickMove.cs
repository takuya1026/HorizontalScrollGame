
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
    private GimmickMoveType m_moveType = default;

    [SerializeField, Tooltip("移動の終点位置")]
    private float m_targetValue = default;

    [SerializeField, Tooltip("移動速度")]
    private float m_speed = default;
    private float m_startSpeed = default;

    [SerializeField, Tooltip("クローン時間")]
    private float m_cloneInterval = default;
    private float m_cloneTime = default;

    [SerializeField, Tooltip("クローン最大個数")]
    private float m_cloneMaxCount = default;

    [SerializeField, Tooltip("落下開始時間")]
    private float m_fallStartInterval = default;
    private float m_fallStartTime = default;

    [SerializeField, Tooltip("ブロック再生時間")]
    private float m_fallRegenerationInterval = default;
    private float m_fallRegenerationTime = default;

    private Vector3 m_startPositon = default;
    private Quaternion m_startRotation = default;
    private Vector3 m_targetPositon = default;
    private Vector3[] m_targetRotation = default;
    private bool m_isTurnBack = default;
    private List<GameObject> m_cloneList = default;
    private int m_shakeState = default;
    private int m_shakeCount = default;
    private bool m_isPlayerRiding = default;

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
        m_startRotation = transform.rotation;
        m_startSpeed = m_speed;

        switch (m_moveType)
        {
            case GimmickMoveType.UP_AND_DOWN:
                m_targetPositon = new Vector3(transform.position.x, transform.position.y + m_targetValue, transform.position.z);
                break;
            case GimmickMoveType.LEFT_AND_RIGHT:
                m_targetPositon = new Vector3(transform.position.x + m_targetValue, transform.position.y, transform.position.z);
                break;
            case GimmickMoveType.INFINITE:
                m_cloneList = new List<GameObject>();
                m_cloneTime = m_cloneInterval;
                m_targetPositon = new Vector3(transform.position.x, transform.position.y + m_targetValue, transform.position.z);
                break;
            case GimmickMoveType.FALL:
                m_fallStartTime = m_fallStartInterval;
                m_fallRegenerationTime = m_fallRegenerationInterval;
                m_targetValue = -100;
                m_targetPositon = new Vector3(transform.position.x, transform.position.y + m_targetValue, transform.position.z);
                m_targetRotation = new Vector3[2];
                m_targetRotation[0] = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + 5.0f);
                m_targetRotation[1] = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z - 5.0f);
                m_shakeState = 0;
                m_shakeCount = 0;
                m_isPlayerRiding = true;
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
            case GimmickMoveType.UP_AND_DOWN:
                moveUpAndDown();
                break;
            case GimmickMoveType.LEFT_AND_RIGHT:
                moveLeftAndRight();
                break;
            case GimmickMoveType.INFINITE:
                moveInfinite();
                break;
            case GimmickMoveType.FALL:
                fall();
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
            m_isTurnBack = !m_isTurnBack;
            m_targetPositon.y = (m_isTurnBack) ? m_startPositon.y : transform.position.y + m_targetValue;
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
            m_isTurnBack = !m_isTurnBack;
            m_targetPositon.x = (m_isTurnBack) ? m_startPositon.x : transform.position.x + m_targetValue;
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, m_targetPositon, m_speed * Time.deltaTime);
    }

    /// <summary>
    /// 上下に無限移動
    /// </summary>
    private void moveInfinite()
    {
        if (m_cloneTime <= 0.0f)
        {
            if (m_cloneList.Count >= m_cloneMaxCount)
            {
                for (int i = 0, length = m_cloneList.Count; i < length; ++i)
                {
                    if (m_cloneList[i].transform.position.y == m_targetPositon.y)
                    {
                        m_cloneList[i].transform.position = m_startPositon;
                        break;
                    }
                }
            }
            else
            {
                // クローンを作成
                GameObject clone = GameObject.CreatePrimitive(PrimitiveType.Cube);
                clone.transform.parent = gameObject.transform;
                clone.transform.position = gameObject.transform.position;
                clone.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                clone.transform.rotation = gameObject.transform.rotation;

                MeshRenderer objectMeshRenderer = gameObject.GetComponent<MeshRenderer>();
                MeshRenderer cloneMeshRenderer = clone.GetComponent<MeshRenderer>();

                cloneMeshRenderer.material = objectMeshRenderer.material;

                m_cloneList.Add(clone);
            }

            m_cloneTime = m_cloneInterval;
        }

        for (int i = 0, length = m_cloneList.Count; i < length; ++i)
        {
            m_cloneList[i].transform.position = Vector3.MoveTowards(m_cloneList[i].transform.position, m_targetPositon, m_speed * Time.deltaTime);
        }

        m_cloneTime--;
    }

    /// <summary>
    /// 落下
    /// </summary>
    private void fall()
    {
        if (m_isPlayerRiding && ! m_isTurnBack)
        {
            if (m_fallStartTime <= 0.0f)
            {
                const int maxCount = 19;
                if (m_shakeCount == maxCount)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, m_startRotation, 0.8f);
                    if (Mathf.DeltaAngle(Mathf.Round(transform.eulerAngles.z), m_startRotation.z) == 0)
                    {
                        m_shakeState = 0;
                        m_shakeCount = 0;
                        m_fallStartTime = m_fallStartInterval;
                        m_isTurnBack = true;
                    }
                }
                else
                {
                    transform.rotation =  Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(m_targetRotation[m_shakeState]), 0.8f);
                    if (Mathf.DeltaAngle(Mathf.Round(transform.eulerAngles.z), m_targetRotation[m_shakeState].z) == 0)
                    {
                        m_shakeState++;
                        if (m_shakeState >= 2)
                        {
                            m_shakeState = 0;
                        }
                        m_shakeCount++;
                    }
                }
                return;
            }

            m_fallStartTime--;
        }
        else if (m_isTurnBack)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_targetPositon, m_speed * Time.deltaTime);
            m_speed += 0.2f;

            if (m_fallRegenerationTime <= 0.0f)
            {
                m_fallRegenerationTime = m_fallRegenerationInterval;
                transform.position = m_startPositon;
                m_speed = m_startSpeed;
                m_isTurnBack = false;
                return;
            }

            if (transform.position.y == m_targetPositon.y)
            {
                m_fallRegenerationTime--;
            }
        }
    }
}
