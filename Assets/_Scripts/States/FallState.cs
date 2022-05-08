using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : MovementState
{
    public float gravityModifier = 0.5f;

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
        movementData.currentVelocity = agent.rb2d.velocity;
        movementData.currentVelocity.y += gravityModifier * Physics2D.gravity.y * Time.deltaTime;
        agent.rb2d.velocity = movementData.currentVelocity;

        CalculateVelocity();
        SetPlayerVelocity();

        if (agent.groundDetector.isGrounded)
        {
            agent.TransitionToState(IdleState);
        }
    }
}
