using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : State
{
    [SerializeField]
    public State IdleState;

    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.run);

        agent.agentData.horizontalMovementDirection = 0;
        agent.agentData.currentSpeed = 0;
        agent.agentData.currentVelocity = Vector2.zero;
    }

    public override void StateUpdate()
    {
        if (TestFallTransition())
            return;

        base.StateUpdate();
        CalculateVelocity();
        SetPlayerVelocity();

        if (Mathf.Abs(agent.rb2d.velocity.x) < 0.01f)
        {
            agent.TransitionToState(IdleState);
        }

    }

    protected void SetPlayerVelocity()
    {
        agent.rb2d.velocity = agent.agentData.currentVelocity;
    }

    protected void CalculateVelocity()
    {
        CalculateSpeed(agent.agentInput.MovementVector);
        CalculateHorizontalDirection();
        agent.agentData.currentVelocity = Vector3.right * agent.agentData.horizontalMovementDirection *
            agent.agentData.currentSpeed;
        agent.agentData.currentVelocity.y = agent.rb2d.velocity.y;
    }

    protected void CalculateHorizontalDirection()
    {
        if (agent.agentInput.MovementVector.x > 0)
        {
            agent.agentData.horizontalMovementDirection = 1;
        }
        else if (agent.agentInput.MovementVector.x < 0)
        {
            agent.agentData.horizontalMovementDirection = -1;
        }
    }

    protected void CalculateSpeed(Vector2 movementVector)
    {
        if (Mathf.Abs(movementVector.x) > 0)
        {
            agent.agentData.currentSpeed += agent.agentData.acceleration * Time.deltaTime;
        }
        else
        {
            agent.agentData.currentSpeed -= agent.agentData.deacceleration * Time.deltaTime;
        }
        agent.agentData.currentSpeed = Mathf.Clamp(agent.agentData.currentSpeed, 0, agent.agentData.maxSpeed);
    }
}
