using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : MovementState
{
    [SerializeField] protected State ClimbState;
    
    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.fall);
    }

    protected override void HandleJumpPressed()
    {
        //Don't allow jumping fall state
    }

    public override void StateUpdate()
    {
        agent.agentData.currentVelocity = agent.rb2d.velocity;
        agent.agentData.currentVelocity.y += agent.agentData.gravityModifier * Physics2D.gravity.y * Time.deltaTime;
        agent.rb2d.velocity = agent.agentData.currentVelocity;

        CalculateVelocity();
        SetPlayerVelocity();

        if (agent.groundDetector.isGrounded)
        {
            agent.TransitionToState(IdleState);
        }
        else if (agent.climbingDetector.CanClimb && Mathf.Abs(agent.agentInput.MovementVector.y) > 0)
        {
            agent.TransitionToState(ClimbState);
        }
    }
}
