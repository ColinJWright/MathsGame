using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Idle = 0,
        Walking = 1,
        Flying = 2,
        Stunned = 3
    }

    // player's current state
    public PlayerState curState;

    // player's horizontal velocity
    public float moveSpeed;

    // player's upwards jetpack force
    public float flyingForce;

    // is the player on the ground?
    private bool grounded;

    // duration of stun
    public float stunDuration;
    private float stunStartTime;

    // components
    public Rigidbody2D rig;
    public Animator anim;
    public ParticleSystem jetpackParticle;

    void FixedUpdate ()
    {
        grounded = IsGrounded();

        CheckInputs();

        // is the player stunned?
        if(curState == PlayerState.Stunned)
        {
            // has the player been stunned for the duration
            if(Time.time - stunStartTime >= stunDuration)
            {
                curState = PlayerState.Idle;
            }
        }
    }

    // checks for user input
    void CheckInputs ()
    {
        if(curState != PlayerState.Stunned)
        {
            Move();

            //flying
            if(Input.GetKey(KeyCode.UpArrow))
                Fly();
            else
                jetpackParticle.Stop();
        }

        // update our state
        SetState();
    }

    // setting the player's state and changing animation
    void SetState ()
    {
        // not do anything if the player is stunned
        if(curState != PlayerState.Stunned)
        {
            // idle
            if(rig.velocity.magnitude == 0 && grounded)
                curState = PlayerState.Idle;
            // walking
            if(rig.velocity.x != 0 && grounded)
                curState = PlayerState.Walking;
            // flying
            if(rig.velocity.magnitude != 0 && !grounded)
                curState = PlayerState.Flying;
        }

        // tell the animator we've changed states
        anim.SetInteger("State", (int)curState);
    }

    // move the player horizontally
    void Move ()
    {
        // get horizontal axis
        float dir = Input.GetAxis("Horizontal");

        // flip player to face moving direction
        if(dir > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if(dir < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // set the rigidbody velocity
        rig.velocity = new Vector2(dir * moveSpeed, rig.velocity.y);
    }

    // add force upwards to the player
    void Fly ()
    {
        // add force upwards
        rig.AddForce(Vector2.up * flyingForce, ForceMode2D.Impulse);

        // play jet pack particle
        if(!jetpackParticle.isPlaying)
            jetpackParticle.Play();
    }

    // stuns the player
    public void Stun ()
    {
        curState = PlayerState.Stunned;
        rig.velocity = Vector2.down * 3;
        stunStartTime = Time.time;
        jetpackParticle.Stop();
    }

    // returns true if grounded, false otherwise
    bool IsGrounded ()
    {
        // shoot a raycast down underneath the player
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.55f), Vector2.down, 0.3f);

        // did we hit anything
        if(hit.collider != null)
        {
            // was it the floor?
            if(hit.collider.CompareTag("Floor"))
            {
                return true;
            }
        }

        return false;
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        if(curState != PlayerState.Stunned)
        {
            // did we trigger an obstacle?
            if(collision.gameObject.CompareTag("Obstacle"))
            {
                Stun();
            }
        }
    }
}