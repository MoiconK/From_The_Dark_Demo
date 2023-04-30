using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_StunState : StunState
{
    private Boss boss;
    public Boss_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Boss boss) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.boss = boss;
    }

    public override void DoChecks()
    {
        base.DoChecks();
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

        if (isStunTimeOver)
        {
            if (performedCloseRangeAction)
            {
                stateMachine.ChangeState(boss.meleeAttackState);
            }
            else if (isPlayerInMingAggroRange)
            {
                stateMachine.ChangeState(boss.chargeState);
            }
            else
            {
                boss.lookForPlayerState.SetTurnImmediately(true);
                stateMachine.ChangeState(boss.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
