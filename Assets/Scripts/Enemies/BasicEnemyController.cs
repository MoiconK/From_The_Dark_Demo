using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    private enum State { 
    Walking,
    Knockback,
    Dead

    }
    private State currentState;
    
    [SerializeField] private float groundCheckDistance, wallCheckDistance, movementSpeed, maxHealth, knockbackDuration;

    [SerializeField] Vector2 knockbackSpeed;
    
    [SerializeField] private Transform groundCheck, wallCheck;
    
    [SerializeField] private LayerMask whatIsGround;
    
    private bool groundDetected;
    private bool wallDetected;
    
    private int facingDirection;
    private int damageDirection;
    
    private Vector2 movement;
    
    private GameObject alive;
    private Rigidbody2D aliveRB;
    private Animator aliveAnim;

    private float currentHealth, knockbackStartTime;

    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRB = alive.GetComponent<Rigidbody2D>();
        aliveAnim= GetComponent<Animator>();   

        facingDirection= 1;
    }

    private void Update()
    {
        switch(currentState)
        {
            case State.Walking:
                UpdateWalkingState(); 
                break;
            case State.Knockback:
                UpdateKnockbackState(); 
                break;
            case State.Dead:
                UpdateDeadState(); 
                break;
        }
    }

    //--Walking State----

    private void EnterWalkingState()
    {

    }

    private void UpdateWalkingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, whatIsGround);

        if(!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            movement.Set(movementSpeed * facingDirection, aliveRB.velocity.y);
            aliveRB.velocity = movement;
        }
    }

    private void ExitWalkingState()
    {

    }

    //---Knockback State------

    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        aliveRB.velocity = movement;
        aliveAnim.SetBool("knockback", true);
    }

    private void UpdateKnockbackState()
    {
        if(Time.time >= knockbackStartTime + knockbackDuration)
        {
            SwitchState(State.Walking);
        }
    }

    private void ExitKnockbackState()
    {
        aliveAnim.SetBool("knockback", false);
    }

    //---Dead State---------
    private void EnterDeadState()
    {
        //ded
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    //---Other functions-------------
    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];

        if (attackDetails[1] > alive.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        //Hit particle

        if(currentHealth > 0.0f)
        {
            SwitchState(State.Knockback);
        } else if (currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
        }
    }

    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Walking:
                ExitWalkingState();
                break;
                    case State.Knockback:
                    ExitKnockbackState();
                    break;
                        case State.Dead:
                        ExitDeadState();
                        break;
        }
        switch (state)
        {
            case State.Walking:
                EnterWalkingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState= state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}
