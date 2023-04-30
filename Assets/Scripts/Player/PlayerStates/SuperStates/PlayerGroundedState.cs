using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int XInput;

    protected Stats Stats { get => stats ??= core.GetCoreComponent<Stats>(); }
    protected Stats stats;
    protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    protected Movement movement;
    private CollisionSenses CollisionSenses { get => collisionSenses ??= core.GetCoreComponent<CollisionSenses>(); }
    private CollisionSenses collisionSenses;

    private bool jumpInput;
    protected bool dodgeInput;
    private bool isGrounded;
    
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (CollisionSenses)
        {
            isGrounded = CollisionSenses.Grounded;
        }
        
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
        dodgeInput = player.InputHandler.DodgeInput;
        if (player.InputHandler.AttackInputs[(int)CombatInputs.primary] && isGrounded)
        {
            stateMachine.ChangeState(player.PrimaryAttackState);

        } else if (player.InputHandler.AttackInputs[(int)CombatInputs.secondary] && isGrounded)
        {
            stateMachine.ChangeState(player.SecondaryAttackState);  
        }
        else if (player.InputHandler.AttackInputs[(int)CombatInputs.special] && isGrounded && Stats.currentAwakening == 100)
        {
            stateMachine.ChangeState(player.AwakeningAttackState);
            Stats.DecreaseAwakening();
        }
        else if (dodgeInput)
        {
            player.InputHandler.UseDodgeInput();
            stateMachine.ChangeState(player.DodgeState);
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
