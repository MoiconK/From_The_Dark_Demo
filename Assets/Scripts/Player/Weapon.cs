using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Weapon : MonoBehaviour
{
    private List<IDamageable> detectedDamageables = new List<IDamageable>();
    private List<IKnockbackable> detectedKnockbackables = new List<IKnockbackable>();

    [SerializeField] private SO_WeaponData weaponData;

    protected Animator lightCombatAnimator;
    protected PlayerAttackState state;
    protected Core core;

    protected int attackCounter;

    protected virtual void Awake()
    {
        lightCombatAnimator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }
    private void CheckMeleeAttack()
    {
        WeaponAttackDetails details= weaponData.AttackDetails[attackCounter];

        foreach (IDamageable item in detectedDamageables.ToList())
        {
           item.Damage(details.damageAmount);

        }

        foreach (IKnockbackable item in detectedKnockbackables.ToList())
        {
            item.Knockback(details.knockbackAngle, details.knockbackStrength, core.Movement.FacingDirection);
        }
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        if(attackCounter >= weaponData.amountOfAttacks)
        {
            attackCounter = 0;
        }


        lightCombatAnimator.SetBool("attack", true);
        lightCombatAnimator.SetInteger("attackCounter", attackCounter);
    }

    public virtual void ExitWeapon()
    {
        lightCombatAnimator.SetBool("attack", false);

        attackCounter++;

        gameObject.SetActive(false);
        
    }
    public void AddToDetected(Collider2D collision)
    {
        
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if(damageable != null)
        {
            
            detectedDamageables.Add(damageable);
        }

        IKnockbackable knockbackable= collision.GetComponent<IKnockbackable>();

        if(knockbackable != null)
        {
            detectedKnockbackables.Add(knockbackable);
        }
    }

    public void RemoveFromDetected(Collider2D collision)
    {
        
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            
            detectedDamageables.Remove(damageable);
        }

        IKnockbackable knockbackable = collision.GetComponent<IKnockbackable>();

        if (knockbackable != null)
        {
            detectedKnockbackables.Remove(knockbackable);
        }
    }

    #region Animation Triggers

    public virtual void AnimationFinishTrigger()
    {
        state.AnimationFinishTrigger();
    }

    public virtual void AnimationStartMovementTrigger()
    {
        state.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
    }

    public virtual void AnimationStopMovementTrigger()
    {
        state.SetPlayerVelocity(0);
    }

    public virtual void AnimationTurnOffFlipTrigger()
    {
        state.SetFlipCheck(false);
    }

    public virtual void AnimationTurnOnFlipTrigger()
    {
        state.SetFlipCheck(true);
    }

    public virtual void AnimationActionTrigger()
    {
        CheckMeleeAttack();
    }

    #endregion

    public void InitializeWeapon(PlayerAttackState state, Core core)
    {
        this.state= state;
        this.core= core;
    }
}
