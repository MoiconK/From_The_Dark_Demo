using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D RB { get; private set; }
    private Vector2 workspace;
    public int FacingDirection { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        FacingDirection = 1;
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
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    #endregion
    private void Flip()
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
