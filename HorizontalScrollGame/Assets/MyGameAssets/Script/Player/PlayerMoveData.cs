using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObject/PlayerData")]
public class PlayerMoveData:ScriptableObject
{
    [SerializeField] private float m_moveSpeed = default;
    [SerializeField] private float m_jumpPowerPhase1 = default;
    [SerializeField] private float m_jumpPowerPhase2 = default;
    [SerializeField] private float m_playerGravity = default;
    [SerializeField] private float m_rayRange = default;

    public float m_GetMoveSpeed => m_moveSpeed;
    public float m_GetJumpPowerPhase1 => m_jumpPowerPhase1;
    public float m_GetJumpPowerPhase2 => m_jumpPowerPhase2;
    public float m_GetPlayerGravity => m_playerGravity;
    public float m_GetRayRange => m_rayRange;
}
