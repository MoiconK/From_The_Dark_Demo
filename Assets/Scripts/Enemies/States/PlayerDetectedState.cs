using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    protected Movement movement;
    private CollisionSenses CollisionSenses { get => collisionSenses ??= core.GetCoreComponent<CollisionSenses>(); }
    private CollisionSenses collisionSenses;

    protected D_PlayerDetectedState stateData;

    protected bool isPlayerInMinAggroRange;
    protected bool isPlayerInMaxAggroRange;
    protected bool performLongRangeAction;
    protected bool performCloseRangeAction;
    protected bool isDetectingLedge;

    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isDetectingLedge = CollisionSenses.CheckLedge;
    }

    public override void Enter()
    {
        base.Enter();
        performLongRangeAction= false;
        Movement?.SetVelocityX(0f);

        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.SetVelocityX(0f);

        if (Time.time >= startTime + stateData.longRangeActionTime)
        {
            performLongRangeAction= true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
   
    }
}
