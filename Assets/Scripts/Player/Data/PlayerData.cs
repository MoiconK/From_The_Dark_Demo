using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data") ]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]

    public float movementVelocity = 10;

    [Header("Jump State")]

    public float jumpVelocity = 15;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float JumpHeightMultiplier = 0.5f;

    [Header("Check Variables")]

    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;

    [Header("Attack Variables")]
    public int damage;
}
