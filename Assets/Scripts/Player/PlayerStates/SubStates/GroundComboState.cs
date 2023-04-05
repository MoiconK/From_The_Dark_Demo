using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundComboState : MeleeBaseState
{
    public GroundComboState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        attackIndex = 2;
        duration = 0.5f;
        if (Time.deltaTime >= duration)
        {
            if (shouldCombo)
            {
                stateMachine.ChangeState(player.ComboState);
            }
            else
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
}
