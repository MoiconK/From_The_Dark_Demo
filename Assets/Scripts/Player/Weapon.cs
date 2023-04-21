using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEditor.Progress;

public class Weapon : MonoBehaviour
{
    protected Combat Combat { get => combat ??= core.GetCoreComponent<Combat>(); }
    protected Combat combat;
    protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    protected Movement movement;

    private List<IDamageable> detectedDamageables = new List<IDamageable>();
    private List<IKnockbackable> detectedKnockbackables = new List<IKnockbackable>();

    [SerializeField] private SO_WeaponData weaponData;

    protected GameObject weapon;

    protected GameObject awakeningAttack;
    protected Animator awakeningCombat;

    protected GameObject lightAttack;
    protected Animator lightCombat;

    protected GameObject heavyAttack;
    protected Animator heavyCombat;

    protected PlayerAttackState state;
    protected Core core;

    protected int attackCounter;

    protected virtual void Awake()
    {
        weapon = GameObject.Find("Weapon");
        lightAttack = weapon.transform.GetChild(0).gameObject;
        heavyAttack = weapon.transform.GetChild(1).gameObject;
        awakeningAttack = weapon.transform.GetChild(2).gameObject;
        lightCombat = lightAttack.GetComponent<Animator>();
        heavyCombat = heavyAttack.GetComponent<Animator>();
        awakeningCombat = awakeningAttack.GetComponent<Animator>();
        gameObject.SetActive(false);
    }
    private void CheckMeleeAttack()
    {
       
        WeaponAttackDetails details= weaponData.AttackDetails[attackCounter];       

        foreach (IDamageable item in detectedDamageables.ToList())
        {
           item.Damage(details.damageAmount);
           Combat.AwakeningCharge(details.awakeningRecharge);

        }

        foreach (IKnockbackable item in detectedKnockbackables.ToList())
        {
            item.Knockback(details.knockbackAngle, details.knockbackStrength, Movement.FacingDirection);
        }
    }

    public virtual void EnterWeapon()
    {
        

        
        gameObject.SetActive(true);

        if(attackCounter >= weaponData.amountOfAttacks)
        {
            attackCounter = 0;
        }


        lightCombat.SetBool("attack", true);
        heavyCombat.SetBool("attack", true);
        awakeningCombat.SetBool("attack", true);
        
        lightCombat.SetInteger("attackCounter", attackCounter);
        heavyCombat.SetInteger("attackCounter", attackCounter);
        awakeningCombat.SetInteger("attackCounter", attackCounter);
    }

    public virtual void ExitWeapon()
    {
        lightCombat.SetBool("attack", false);
        heavyCombat.SetBool("attack", false);
        awakeningCombat.SetBool("attack", false);

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
