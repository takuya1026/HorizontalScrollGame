using UnityEngine;

public enum JoypadInputType
{
    JOYPAD_BUTTON_A,
    JOYPAD_BUTTON_B,
    JOYPAD_BUTTON_X,
    JOYPAD_BUTTON_Y,
    JOYPAD_BUTTON_START,
    JOYPAD_BUTTON_BACK,
    JOYPAD_BUTTON_LB,
    JOYPAD_BUTTON_RB,
    JOYPAD_BUTTON_STICK_LEFT, 
    JOYPAD_BUTTON_STICK_RIGHT,

    JOYPAD_TRIGGER_L,
    JOYPAD_TRIGGER_R,
    JOYPAD_LEFT_STICK_HORIZONTAL,
    JOYPAD_LEFT_STICK_VERTICAL,
    JOYPAD_RIGHT_STICK_HORIZONTAL,
    JOYPAD_RIGHT_STICK_VERTICAL,
    JOYPAD_DIR_HORIZONTAL,
    JOYPAD_DIR_VERTICAL,

    INPUT_MAX,
}

public enum PushDirection
{
    NONE,
    UP,
    DOWN,
    RIGHT,
    LEFT,
}

public enum PushType
{
    NONE,
    PUSH,
    RELEASE,
    KEEP_PUSH,
}

enum InputType
{
    JUMP,
    RUN,
    ATTACK,
}

public class InputManager : SingletonMonoBehaviour<InputManager>
{
    [SerializeField, Header("操作可能領域を設定")]
    private float m_inputArea = 0.3f;

    public float m_GetInputArea => m_inputArea;

    private Vector2 m_leftStick;
    private Vector2 m_rightStick;
    private Vector2 m_dirButton;
    private Vector2 m_keyboardCross;

    public Vector2 m_GetKeyboardCross => m_keyboardCross;

    /// <summary>
    /// 左スティックの入力値を返す
    /// </summary>
    public Vector2 m_GetLeftStick => m_leftStick;

    /// <summary>
    /// 右スティックの入力値を返す
    /// </summary>
    public Vector2 m_GetRightStick => m_rightStick;

    /// <summary>
    /// 十字ボタンの入力値を返す
    /// </summary>
    public Vector2 m_GetDirectButton => m_dirButton;

    /// <summary>
    /// 左スティックの更新
    /// </summary>
    private void updateLeftStick()
    {
        m_leftStick.x = Input.GetAxis(JoypadInputType.JOYPAD_LEFT_STICK_VERTICAL.ToString());
        m_leftStick.y = Input.GetAxis(JoypadInputType.JOYPAD_LEFT_STICK_HORIZONTAL.ToString());
    }
    /// <summary>
    /// 右スティックの更新
    /// </summary>
    private void updateRightStick()
    {
        m_rightStick.x = Input.GetAxis(JoypadInputType.JOYPAD_RIGHT_STICK_VERTICAL.ToString());
        m_rightStick.y = Input.GetAxis(JoypadInputType.JOYPAD_RIGHT_STICK_HORIZONTAL.ToString());
    }

    /// <summary>
    /// 十字ボタンの更新
    /// </summary>
    private void updateDirectButton()
    {
        m_dirButton.x = Input.GetAxis(JoypadInputType.JOYPAD_DIR_VERTICAL.ToString());
        m_dirButton.y = Input.GetAxis(JoypadInputType.JOYPAD_DIR_HORIZONTAL.ToString());
    }

    /// <summary>
    /// Joypadの左、右スティク、左の十字キーの入力を取る
    /// </summary>
    private void updateJoypad()
    {
        updateLeftStick();
        updateRightStick();
        updateDirectButton();
    }

    /// <summary>
    /// キーボードの左右のみの入力を取る
    /// </summary>
    private void updateKeyboardClass()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            m_keyboardCross.x = Vector2.left.x;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            m_keyboardCross.x = Vector2.right.x;
        }
        else
        {
            m_keyboardCross.x = Vector2.zero.x;
        }

        m_keyboardCross.y = Vector2.zero.y;
    }

    /// <summary>
    /// ボタンの入力状態を取る
    /// 
    /// 例：Aボタンを押した"瞬間"の情報を取る
    /// if(GetButtonsPushType(JoypadInputType(JoypadInputType.JOYPAD_BUTTON_A) == PushType.PUSH)
    /// {
    ///     Debug.Log("押されています");
    /// }
    /// 
    /// </summary>
    /// <param name="buttonType"></param>
    /// <returns></returns>
    public PushType GetButtonsPushType(JoypadInputType buttonType)
    {
        string buttonStr = buttonType.ToString();

        if(Input.GetButtonDown(buttonStr))
        {
            return PushType.PUSH;
        }
        if(Input.GetButton(buttonStr))
        {
            return PushType.KEEP_PUSH;
        }
        if(Input.GetButtonUp(buttonStr))
        {
            return PushType.RELEASE;
        }

        return PushType.NONE;
    }

    public PushType GetKeyboardPushType(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            return PushType.PUSH;
        }
        if (Input.GetKeyDown(key))
        {
            return PushType.KEEP_PUSH;
        }
        if (Input.GetKeyDown(key))
        {
            return PushType.RELEASE;
        }

        return PushType.NONE;
    }

    private void Update()
    {
        updateJoypad();
        updateKeyboardClass();
    }

    /// <summary>
    /// 入力する方向を返す
    /// </summary>
    /// <returns></returns>
    public PushDirection GetPushDirection()
    {
        //Joypadの左右スティック
        bool isRight =  (m_GetLeftStick.x  > m_inputArea) ||
                        (m_GetRightStick.x > m_inputArea);

        bool isLeft =   (m_GetLeftStick.x  < -m_inputArea) ||
                        (m_GetRightStick.x < -m_inputArea);

        bool isDown =   (m_GetLeftStick.y  > m_inputArea) ||
                        (m_GetRightStick.y > m_inputArea);

        bool isUp   =   (m_GetLeftStick.y  < -m_inputArea) ||
                        (m_GetRightStick.y < -m_inputArea);

        //JoypadのDPad
        bool isDRight   = m_dirButton.x >  m_inputArea;
        bool isDLeft    = m_dirButton.x < -m_inputArea;
        bool isDDown    = m_dirButton.y < -m_inputArea;
        bool isDUp      = m_dirButton.y >  m_inputArea;

        if (isRight || isDRight) 
        {
            return PushDirection.RIGHT;
        }
        else if(isLeft || isDLeft)
        {
            return PushDirection.LEFT;
        }
        else if(isDown || isDDown)
        {
            return PushDirection.DOWN;
        }
        else if(isUp || isDUp)
        {
            return PushDirection.UP;
        }

        return PushDirection.NONE;
    }
}
