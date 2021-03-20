using UnityEngine;

public class PlayerAnimation
{
    private Animator m_animator = default;
    
    public void Init(Animator animator)
    {
        m_animator = animator;
    }

    public void Update()
    {
        float inputNotTime = InputManager.m_Instance.m_GetNotInputTime;

        

        if(InputManager.m_Instance.GetButtonsPushType(JoypadInputType.JOYPAD_BUTTON_Y) == PushType.PUSH)
        {
            m_animator.SetInteger("idleCount", GetWaitType());
        }


        if (InputManager.m_Instance.GetButtonsPushType(JoypadInputType.JOYPAD_BUTTON_X) == PushType.PUSH)
        {
            m_animator.SetInteger("idleCount", 0);
        }

    }

    private int GetWaitType()
    {
        //1～４までの数を返す
        return (Random.Range(1, 4 + 1));
    }
}
