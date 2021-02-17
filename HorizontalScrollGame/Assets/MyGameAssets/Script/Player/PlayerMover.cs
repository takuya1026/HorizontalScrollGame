using UnityEngine;

/// <summary>
/// プレイヤーの移動を制御
/// </summary>
public class PlayerMover
{
    private Vector3         m_direction;
    private Vector3         m_velocity;
    private Ray             m_judgRay;
    private PlayerMoveData  m_playerMoveData;
    private Rigidbody       m_rigidbody;
    private Transform       m_playerTransform;
    public  bool            m_isGround;
    public  bool            m_isJump;
    private int             m_jumpCount = 0;

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="mover"></param>移動用のデータ
    /// <param name="rigidbody"></param>物理挙動のコンポーネント
    /// <param name="playerTrasform"></param>プレイヤーのトランスフォーム
    public void Init(PlayerMoveData mover,Rigidbody rigidbody,Transform playerTrasform)
    {
        m_playerMoveData    = mover;
        m_rigidbody         = rigidbody;
        m_isGround          = true;
        m_playerTransform   = playerTrasform;
    }

    /// <summary>
    /// 方向を変更
    /// </summary>
    /// <param name="dir"></param>変更後の方向
    public void OnChageDir(Vector3 dir,bool isJump)
    {
        m_direction = dir;
        m_isJump = isJump;
    }

    /// <summary>
    /// 普通の更新
    /// </summary>
    public void Update()
    {
        UpdateVelocity();
        Jump();
    }

    /// <summary>
    /// m_rigidbodyの更新
    /// </summary>
    public void FixedUpdate()
    {
        m_rigidbody.velocity += (m_velocity * Time.deltaTime);
        SetLocalGravity();
    }

    /// <summary>
    /// 任意の重力を設定
    /// </summary>
    private void SetLocalGravity()
    {
        var gravity = new Vector3(0f, m_playerMoveData.m_GetPlayerGravity, 0f);
        m_rigidbody.AddForce(gravity, ForceMode.Acceleration);
    }

    /// <summary>
    /// 移動量を生成
    /// </summary>
    private void UpdateVelocity()
    {
        InputManager inputManger = InputManager.m_Instance;

        //移動量の算出
        bool isInput = Mathf.Abs(m_direction.magnitude) > inputManger.m_GetInputArea;
        if (isInput)
        {
            m_velocity = (m_direction.normalized * m_playerMoveData.m_GetMoveSpeed);
        }
        else
        {
            m_velocity = Vector3.down;
        }

        m_velocity += m_direction * (m_playerMoveData.m_GetMoveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    private void Jump()
    {
        InputManager inputManger = InputManager.m_Instance;

        //地面についていた
        if (!m_isGround)
        {
            if (IsGround())
            {
                m_isGround = true;
                m_jumpCount = 0;
            }
        }

        //ジャンプ
        if (m_isJump)
        {
            if (m_jumpCount >= 2) return;

            if (!m_isGround)
            {
                m_rigidbody.AddForce(Vector3.up * m_playerMoveData.m_GetJumpPowerPhase2);
                m_jumpCount++;
                return;
            }

            m_rigidbody.AddForce(Vector3.up * m_playerMoveData.m_GetJumpPowerPhase1);

            m_isGround = false;
            m_jumpCount++;
        }
    }

    /// <summary>
    /// 地面についてるかを判定
    /// </summary>
    /// <returns></returns>
    private bool IsGround()
    {
        Vector3 rayStartPos = m_playerTransform.position;
        Vector3 rayEndPos = m_playerTransform.up * -1;
        m_judgRay = new Ray(rayStartPos, rayEndPos);

#if UNITY_EDITOR
        Debug.DrawRay(m_judgRay.origin, m_judgRay.direction * m_playerMoveData.m_GetRayRange, Color.red);
#endif

        bool isHit = Physics.Raycast(m_judgRay.origin, m_judgRay.direction, m_playerMoveData.m_GetRayRange, LayerMask.GetMask("Field"));

        if (isHit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
