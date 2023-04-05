using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Animator lightCombatAnimator;
    protected PlayerAttackState state;

    protected virtual void Start()
    {
        lightCombatAnimator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);
        lightCombatAnimator.SetBool("attack", true);
    }

    public virtual void ExitWeapon()
    {
        lightCombatAnimator.SetBool("attack", false);
        gameObject.SetActive(false);
    }

    #region Animation Triggers

    public virtual void AnimationFinishTrigger()
    {
        state.AnimationFinishTrigger();
    }

    #endregion

    public void InitializeWeapon(PlayerAttackState state)
    {
        this.state= state;
    }
}
