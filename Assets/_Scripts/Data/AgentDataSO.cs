using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AgentData",menuName ="Agent/Data")]
public class AgentDataSO : ScriptableObject
{
    [Header("Movement Data")]
    [Space]
    public int horizontalMovementDirection;
    public float currentSpeed;
    public Vector2 currentVelocity;
    [Space]
    public float maxSpeed = 6;
    public float acceleration = 50;
    public float deacceleration = 50;

    [Header("Jump Data")]
    [Space]
    public float jumpForce = 12;
    public float lowJumpMultiplier = 2;
    public float gravityModifier = 0.5f;
}
