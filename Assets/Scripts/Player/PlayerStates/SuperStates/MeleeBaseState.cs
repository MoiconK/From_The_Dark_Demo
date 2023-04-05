using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBaseState : PlayerAbilityState
{
    public float duration;
    protected bool shouldCombo;
    protected int attackIndex;
    private bool attacklInput;

    public MeleeBaseState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
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
        attacklInput = player.InputHandler.AttackLInput;
        base.LogicUpdate();
        if (attacklInput)
        {
            shouldCombo= true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    
}
