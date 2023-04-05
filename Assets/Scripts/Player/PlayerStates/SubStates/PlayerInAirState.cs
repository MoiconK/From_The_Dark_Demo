using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded;
    private int XInput;
    private bool jumpInputStop;
    private bool coyoteTime;
    private bool isJumping;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded= player.CheckIfGrounded();
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
        CheckCoyoteTime();
        XInput = player.InputHandler.NormInputX;
        jumpInputStop = player.InputHandler.JumpInputStop;
        CheckJumpMultiplier();
        if (player.InputHandler.AttackInputs[(int)CombatInputs.primary])
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
        else if (player.InputHandler.AttackInputs[(int)CombatInputs.secondary])
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }

        else if (isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        } else
        {
            player.CheckIfShouldFlip(XInput);
            player.SetVelocityX(playerData.movementVelocity * XInput);
            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime()
    {
        if(coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime= false;
        }
    }

    public void StartCoyoteTime()
    {
        coyoteTime= true;
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetVelocityY(player.CurrentVelocity.y * playerData.JumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0)
            {
                isJumping = false;
            }
        }
    }

    public void IsJumping() => isJumping = true;
}
