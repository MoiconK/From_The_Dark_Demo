using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEntryState : MeleeBaseState
{
    public GroundEntryState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        attackIndex = 1;
        duration = 0.5f;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.deltaTime >= duration)
        {
            if (shouldCombo)
            {
                stateMachine.ChangeState(player.ComboState);
            } else
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
}
