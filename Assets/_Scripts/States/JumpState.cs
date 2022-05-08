using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : MovementState
{
    private bool jumpPressed = false;
    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.jump);
        agent.agentData.currentVelocity = agent.rb2d.velocity;
        agent.agentData.currentVelocity.y = agent.agentData.jumpForce;
        agent.rb2d.velocity = agent.agentData.currentVelocity;
        jumpPressed = true;
    }

    protected override void HandleJumpPressed()
    {
        jumpPressed = true;
    }

    protected override void HandleJumpReleased()
    {
        jumpPressed = false;
    }

    public override void StateUpdate()
    {
        ControlJumpHeight();
        CalculateVelocity();
        SetPlayerVelocity();
        if (agent.rb2d.velocity.y <= 0)
        {
            agent.TransitionToState(FallState);
        }
    }

    private void ControlJumpHeight()
    {
        if (jumpPressed == false)
        {
            agent.agentData.currentVelocity = agent.rb2d.velocity;
            agent.agentData.currentVelocity.y += agent.agentData.lowJumpMultiplier * Physics2D.gravity.y * Time.deltaTime;
            agent.rb2d.velocity = agent.agentData.currentVelocity;
        }
    }
}
