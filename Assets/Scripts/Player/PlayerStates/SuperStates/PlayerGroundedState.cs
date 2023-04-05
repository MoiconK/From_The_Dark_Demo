using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int XInput;
    private bool jumpInput;
    private bool isGrounded;
    
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
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
        XInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        if (player.InputHandler.AttackInputs[(int)CombatInputs.primary])
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        } else if (player.InputHandler.AttackInputs[(int)CombatInputs.secondary])
        {
            stateMachine.ChangeState(player.SecondaryAttackState);  
        }

        else if (jumpInput)
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
