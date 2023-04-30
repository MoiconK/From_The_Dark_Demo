using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Entity
{
    public Boss_IdleState idleState { get; private set; }
    public Boss_MoveState moveState { get; private set; }
    public Boss_PlayerDetectedState playerDetectedState { get; private set; }
    public Boss_ChargeState chargeState { get; private set; }
    public Boss_LookForPlayerState lookForPlayerState { get; private set; }
    public Boss_MeleeAttackState meleeAttackState { get; private set; }
    public Boss_StunState stunState { get; private set; }
    public Boss_DeadState deadState { get; private set; }

    [SerializeField] Transform meleeAttackPosition;

    [SerializeField] D_IdleState idleStateData;
    [SerializeField] D_MoveState moveStateData;
    [SerializeField] D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] D_ChargeState chargeStateData;
    [SerializeField] D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] D_MeleeAttackState meleeAttackStateData;
    [SerializeField] D_StunState stunStateData;
    [SerializeField] D_DeadState deadStateData;
    public override void Awake()
    {
        base.Awake();
        moveState = new Boss_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Boss_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new Boss_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        chargeState = new Boss_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new Boss_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new Boss_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new Boss_StunState(this, stateMachine, "stunned", stunStateData, this);
        deadState = new Boss_DeadState(this, stateMachine, "dead", deadStateData, this);

    }

    private void Start()
    {
        stateMachine.Initialize(moveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }

}
