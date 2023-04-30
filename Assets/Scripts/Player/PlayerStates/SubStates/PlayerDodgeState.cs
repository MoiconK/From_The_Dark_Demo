using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerDodgeState : PlayerAbilityState
{
    protected int XInput;
    
    

    public PlayerDodgeState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public void Awake()
    {
        GameObject jugador = core.transform.GetChild(2).gameObject;
        Debug.Log(jugador);
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        
    }

    public override void Enter()
    {
        base.Enter();
        //combat.SetActive(false);     
    }

    public override void Exit()
    {
        base.Exit();
        //combat.SetActive(true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (XInput != 0)
        {
            stateMachine.ChangeState(player.MoveState);
        }
        else if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.IdleState);
        }



    }
}
