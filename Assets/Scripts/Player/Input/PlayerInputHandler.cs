using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool DodgeInput { get; private set; }
    public bool DodgeInputStop { get; private set; }
    public bool[] AttackInputs { get; private set; }
    

    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpImputStartTime;
    private float dodgeInputStartTime;
    
    private void Start()
    {
        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInputs = new bool[count];
    }

    private void Update()
    {
        CheckJumpInputHoldTime();        
    }

    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {
       if (PauseMenu.isPaused == false)
        {
            if (context.started)
            {
                AttackInputs[(int)CombatInputs.primary] = true;
            }
            if (context.canceled)
            {
                AttackInputs[(int)CombatInputs.primary] = false;
            }
        }
    }

    public void OnSecondaryAttackInput(InputAction.CallbackContext context)
    {
        if (PauseMenu.isPaused == false)
        {
            if (context.started)
            {
                AttackInputs[(int)CombatInputs.secondary] = true;
            }
            if (context.canceled)
            {
                AttackInputs[(int)CombatInputs.secondary] = false;
            }
        }
    }

    public void OnSpecialAttackInput(InputAction.CallbackContext context)
    {
        if (PauseMenu.isPaused == false)
        {
            if (context.started)
            {
                AttackInputs[(int)CombatInputs.special] = true;
            }
            if (context.canceled)
            {
                AttackInputs[(int)CombatInputs.special] = false;
            }
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context) 
    {  
        if (PauseMenu.isPaused == false) { 
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context) 
    {
        if (PauseMenu.isPaused == false)
        {
            if (context.started)
        {
            JumpInput = true;
            jumpImputStartTime= Time.time;
            JumpInputStop= false;
        }

        if (context.canceled)
        {
            JumpInputStop= true;
            
        }
        }
    }
   
    public void UseJumpInput() => JumpInput = false;

    private void CheckJumpInputHoldTime()
    {
        if(Time.time >= jumpImputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    public void OnDodgeInput(InputAction.CallbackContext context)
    {
        if (PauseMenu.isPaused == false) { 
            if (context.started)
        {
            DodgeInput = true;
        }

        }
    }

    public void UseDodgeInput() => DodgeInput= false;


}

    

public enum CombatInputs
{
    primary,
    secondary,
    special
}