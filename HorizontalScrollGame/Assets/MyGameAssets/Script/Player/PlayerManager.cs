using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public enum PlayerState
    {
        IDLE,       //待機
        LEFT_RUN,   //左走行
        RIGHT_RUN,  //右左走行
        JUMP,       //ジャンプ
        DAMAGE,     //ダメージ受ける
        DEATH,      //死亡
    }

    [SerializeField] private PlayerMoveData m_playerMoveData = default;
    [SerializeField] private Rigidbody      m_rigidbody      = default;
    [SerializeField] private Animator       m_animator       = default;

    private PlayerInput     m_playerInput      = new PlayerInput();
    private PlayerMover     m_playerMover      = new PlayerMover();
    private PlayerAnimation m_playerAnimation  = new PlayerAnimation();

    private void Awake()
    {
        m_playerInput.Init(OnChangeDir);
        m_playerMover.Init(m_playerMoveData, m_rigidbody,transform);
        m_playerAnimation.Init(m_animator);
    }

    private void OnChangeDir(Vector3 dir ,bool isJump)
    {
        m_playerMover.OnChageDir(dir,isJump);
    }
    private void Update()
    {
        m_playerInput.Update();
        m_playerMover.Update();
        m_playerAnimation.Update();
    }
    private void FixedUpdate()
    {
        m_playerMover.FixedUpdate();
    }
}
