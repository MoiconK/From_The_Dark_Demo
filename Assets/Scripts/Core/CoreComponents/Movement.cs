using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D RB { get; private set; }

    private Vector2 workspace;
    public int FacingDirection { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    public bool CanSetVelocity { get; set;}

    protected override void Awake()
    {
        base.Awake();

        FacingDirection = 1;

        CanSetVelocity = true;

        RB = GetComponentInParent<Rigidbody2D>();
    }
    public void LogicUpdate()
    {
        CurrentVelocity = RB.velocity;
    }

    #region Funciones Set
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        SetFinalVelocity();
        
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        SetFinalVelocity();
    }

    public virtual void SetVelocityKb(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, velocity * angle.y);
        SetFinalVelocity();
        
    }

    private void SetFinalVelocity()
    {
        if(CanSetVelocity) {
            RB.velocity = workspace;
            CurrentVelocity = workspace;
        }
        
    }

    #endregion
    public void Flip()
    {
        FacingDirection *= -1;
        RB.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    public void CheckIfShouldFlip(int xinput)
    {
        if (xinput != 0 && xinput != FacingDirection)
        {
            Flip();
        }
    }
}
