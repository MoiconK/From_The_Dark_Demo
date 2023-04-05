using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFinisherState : MeleeBaseState
{
    public GroundFinisherState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        attackIndex = 3;
        duration = 0.5f;
        if (Time.deltaTime >= duration)
        {
            
                stateMachine.ChangeState(player.IdleState);
                isAbilityDone= true;
            
        }
    }
}
