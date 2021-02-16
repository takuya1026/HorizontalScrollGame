using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private float m_moveSpeed = 10f;
    [SerializeField] private float m_jumpPowerPhase1 = default;
    [SerializeField] private float m_jumpPowerPhase2 = default;
    [SerializeField] private float m_playerGravity = default;
    [SerializeField] private float rayRange = default;

    [SerializeField] private float m_accele = default;
    [SerializeField] private float m_maxSpeed = default;

    Ray ray;
    private Vector3 m_direction;
    private Vector3 m_velocity;
    private Vector3 m_localGravity;
    private Rigidbody m_rigidbody;
    public bool m_isGround;

    private int m_jumpCount = 0;


    private float GRAVITY = 9.8f;


    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        //m_rigidbody.useGravity = false;
        //m_rigidbody.isKinematic = true;
        m_isGround = true;
        m_localGravity = new Vector3(0f, m_playerGravity, 0f);

    }

    // Update is called once per frame
    void Update()
    {
        InputManager inputManger = InputManager.m_Instance;
        
        Input();

        //ワールド座標へ
        ///m_direction = transform.TransformDirection(m_direction);

        //m_velocity.y += GRAVITY;

        
        //移動量の算出
        bool isInput = Mathf.Abs(m_direction.magnitude) > inputManger.m_GetInputArea; 
        if(isInput)
        {
            m_velocity += (m_direction.normalized * m_moveSpeed);
        }
        else
        {
            m_velocity = Vector3.down;
        }
        
       
    }

    private void Input()
    {
        InputManager inputManger = InputManager.m_Instance;
        Vector2 input = Vector2.zero;

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
        if (inputManger.GetButtonsPushType(JoypadInputType.JOYPAD_BUTTON_A) == PushType.PUSH)
        {
            if (m_jumpCount >= 2) return;

            if(!m_isGround)
            {
                m_rigidbody.AddForce(Vector3.up * m_jumpPowerPhase2);
                m_jumpCount++;
                return;
            }

            //TODO:ダブルジャンプの処理
            m_rigidbody.AddForce(Vector3.up * m_jumpPowerPhase1);

            m_isGround = false;
            m_jumpCount++;
        }

        //左ｽﾃｨｯｸ
        if (inputManger.m_GetLeftStick.magnitude > 0f)
        {
            input = inputManger.m_GetLeftStick;
        }
        //十字キー
        else if (inputManger.m_GetDirectButton.magnitude > 0f)
        {
            input = inputManger.m_GetDirectButton;
        }


        //if(input.magnitude)

        //入力を取る
        if (input.x > inputManger.m_GetInputArea)
        {
            m_direction = Vector3.right;
        }
        else if (input.x < -inputManger.m_GetInputArea)
        {
            m_direction = Vector3.left;
        }
        else
        {
            m_direction = Vector3.zero;
        }
        m_velocity = m_direction * (m_moveSpeed * Time.deltaTime);

        m_velocity.y += -GRAVITY;
    }

    public void FixedUpdate()
    {
        //float max = Mathf.Max(m_rigidbody.velocity.magnitude, 20f);


        //transform.position += m_velocity;

        m_rigidbody.velocity += (m_velocity * Time.deltaTime);
        SetLocalGravity();
    }
    private void SetLocalGravity()
    {
        m_rigidbody.AddForce(m_localGravity, ForceMode.Acceleration);
    }

    
    private bool IsGround()
    {
        Vector3 rayStartPos = transform.position;
        Vector3 rayEndPos = transform.up * -1;
        ray = new Ray(rayStartPos, rayEndPos);
        Debug.DrawRay(ray.origin, ray.direction * rayRange , Color.red);

        bool isHit = Physics.Raycast(ray.origin, ray.direction,rayRange, LayerMask.GetMask("Field"));

        Debug.Log(isHit);

        if(isHit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
