﻿using UnityEngine;

/// <summary>
/// 敵のジャンプまわりのパラメータ
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/EnemyMoveJumpParameter")]
public class EnemyMoveJumpParameter : ScriptableObject
{
    [SerializeField]
    private float m_jumpPower = 300;
    public float m_JumpPower => m_jumpPower;

    [SerializeField, Tooltip("※０度は真横")]
    private float m_jumpAngle = 60;
    public float m_JumpAngle => m_jumpAngle;
}
