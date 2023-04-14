using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Entity : MonoBehaviour
{
    protected Movement Movement { get => movement ??= Core.GetCoreComponent<Movement>(); }
    protected Movement movement;

    public FiniteStateMachine stateMachine;

    public D_Entity entityData;

    public int lastDamageDirection { get; private set; }
    public Animator anim { get; private set; }
    public AnimationToStateMachine atsm { get; private set; }
    public Core Core { get; private set; }

    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private Transform groundCheck;

    private float currentHealth;
    private float currentStunResistance;
    private float lastDamageTime;
  

    private Vector2 velocityWorkspace;

    protected bool isStunned;
    protected bool isDead;

    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();

        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;
        
        anim = GetComponent<Animator>();
        atsm = GetComponent<AnimationToStateMachine>();
        
        stateMachine= new FiniteStateMachine();
    }

    public virtual void Update()
    {
        Core.LogicUpdate();
        stateMachine.currentState.LogicUpdate();
        if(Time.time >= lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }   

    public virtual bool CheckPlayerInMinAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.minAggroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.maxAggroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }

    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(Movement.RB.velocity.x, velocity);
        Movement.RB.velocity = velocityWorkspace;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }

       

    public virtual void OnDrawGizmos()
    {
        if(Core != null) { 
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * Movement.FacingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * Movement.FacingDirection * entityData.closeRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * Movement.FacingDirection * entityData.minAggroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * Movement.FacingDirection * entityData.maxAggroDistance), 0.2f);
    }

    }
}
