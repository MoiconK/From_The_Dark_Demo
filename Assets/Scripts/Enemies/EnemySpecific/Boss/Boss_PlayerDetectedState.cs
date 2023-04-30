using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_PlayerDetectedState : PlayerDetectedState
{
    private Boss boss;

    public Boss_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData, Boss boss) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.boss = boss;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(boss.meleeAttackState);
        }
        if (performLongRangeAction)
        {
            stateMachine.ChangeState(boss.chargeState);

        }
        else if (!isPlayerInMaxAggroRange)
        {
            stateMachine.ChangeState(boss.lookForPlayerState);
        }
        else if (!isDetectingLedge)
        {
            Movement.Flip();
            stateMachine.ChangeState(boss.moveState);
        }


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
