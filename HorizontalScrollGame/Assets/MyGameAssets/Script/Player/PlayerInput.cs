using UnityEngine;


public class PlayerInput
{
    private PlayerManager m_player = default;

    private Rigidbody rigidbodyPlayer;

    public void Init(Rigidbody playerRigd)
    {
        //m_player = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
        //m_player
        //rigidbodyPlayer = playerRigd;
    }

    //public PlayerManager.PlayerState GetPlayerRun()
    //{
    //    InputManager inputManager = InputManager.m_Instance;
    //    PushDirection dir = inputManager.GetPushDirection();
    //
    //    if(dir == PushDirection.LEFT)
    //    {
    //        return PlayerManager.PlayerState.LEFT_RUN;
    //    }
    //    if(dir == PushDirection .RIGHT)
    //    {
    //
    //    }
    //
    //}


}
