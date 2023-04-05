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
    public bool AttackLInput { get; private set; }
    public bool AttackLInputStop { get; private set; }

    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpImputStartTime;
    private float attackLImputStartTime;

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckAttackLInputHoldTime();
    }
    public void OnMoveInput(InputAction.CallbackContext context) 
    {  
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context) 
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
    public void OnAttackLInput(InputAction.CallbackContext context)
    {
        if (context.started) {
            AttackLInput = true;
            attackLImputStartTime = Time.time;
            AttackLInputStop = false;
        }
        if (context.canceled)
        {
            AttackLInputStop= true;
        }
        
    }
    public void UseAttackLInput() => AttackLInput=false;
    public void UseJumpInput() => JumpInput = false;

    private void CheckJumpInputHoldTime()
    {
        if(Time.time >= jumpImputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
    private void CheckAttackLInputHoldTime()
    {
        if(Time.time >= attackLImputStartTime + inputHoldTime)
        {
            AttackLInput= false;
        }
    }
}
