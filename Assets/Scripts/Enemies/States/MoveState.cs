using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState stateData;
    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool isPlayerInMinAggroRange;

    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isDetectingLedge = core.CollisionSenses.CheckLedge;
        isDetectingWall = core.CollisionSenses.CheckWall;
    }

    public override void Enter()
    {
        base.Enter();
        core.Movement.SetVelocityX(stateData.movementSpeed * core.Movement.FacingDirection);

        
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();      
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    }
}
