using UnityEngine;
using UnityEngine.Events;

public class PlayerInput
{
    private UnityAction<Vector3,bool>    m_chageInputCallback;
    
    public void Init(UnityAction<Vector3,bool> chageInputCallback)
    {
        m_chageInputCallback = chageInputCallback;
    }

    public void Update()
    {
        Vector2 input   = Vector2.zero;
        Vector3 dir     = Vector3.zero;

        input   = Input(input);
        dir     = GetDirFromInput(dir,input);

        m_chageInputCallback(dir, isJump());
    }

    /// <summary>
    /// 入力を取る
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private Vector2 Input(Vector2 input)
    {
        InputManager inputManger = InputManager.m_Instance;

        float inputCross = Mathf.Abs(inputManger.m_GetKeyboardCross.x);
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
        else if (inputCross > 0f)
        {
            input = inputManger.m_GetKeyboardCross;
        }

        return input;
    }

    /// <summary>
    /// ジャンプをする
    /// </summary>
    /// <returns></returns>
    private bool isJump()
    {
        InputManager inputManger = InputManager.m_Instance;

        //A
        bool isJoypad   = inputManger.GetButtonsPushType(JoypadInputType.JOYPAD_BUTTON_A) == PushType.PUSH;
        //Space
        bool isKeyboard = inputManger.GetKeyboardPushType(KeyCode.Space)                  == PushType.PUSH;

        bool isJump = isJoypad || isKeyboard;
        if (isJump)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 入力から方向を取得
    /// </summary>
    /// <param name="dir"></param>　方向
    /// <param name="input"></param>　入力値
    /// <returns></returns>
    private Vector3 GetDirFromInput(Vector3 dir, Vector2 input)
    {
        InputManager inputManger = InputManager.m_Instance;

        if (input.x > inputManger.m_GetInputArea)
        {
            dir = Vector3.right;
        }
        else if (input.x < -inputManger.m_GetInputArea)
        {
            dir = Vector3.left;
        }
        else
        {
            dir = Vector3.zero;
        }

        return dir;
    }
}
