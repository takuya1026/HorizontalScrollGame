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
    [SerializeField] private Rigidbody m_rigidbody           = default;

    private PlayerInput  m_playerInput     = new PlayerInput();
    private PlayerMover  m_playerMover    = new PlayerMover();

    private void Awake()
    {
        m_playerInput.Init(OnChangeDir);
        m_playerMover.Init(m_playerMoveData, m_rigidbody,transform);
    }

    private void OnChangeDir(Vector3 dir)
    {
        m_playerMover.OnChageDir(dir);
    }
    private void Update()
    {
        m_playerInput.Update();
        m_playerMover.Update();
    }
    private void FixedUpdate()
    {
        m_playerMover.FixedUpdate();
    }
}
