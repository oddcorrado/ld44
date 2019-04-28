using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BULLETS homing front autotarget
// PUNCHES
// AREAS
// CHARGE
// DIVE CHARGE
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputRouter))]
[RequireComponent(typeof(PlayerCondition))]
public class PlayerMovementGround : PlayerMovement
{
    // Rebasables
    private float groundInertia;
    public float GroundInertia { get { return groundInertia; } set { groundInertia = value >= 0 ? value : groundInertiaBase; } }
    private float airInertia;
    public float AirInertia { get { return airInertia; } set { airInertia = value >= 0 ? value : airInertiaBase; } }
    private float walkGroundSpeed;
    public float WalkGroundSpeed { get { return walkGroundSpeed; } set { walkGroundSpeed = value >= 0 ? value : walkGroundSpeedBase; } }
    private float jumpGroundVel;
    public float JumpGroundVel { get { return jumpGroundVel; } set { jumpGroundVel = value >= 0 ? value : jumpGroundVelBase; } }
    private float jumpWallVel;
    public float JumpWallVel { get { return jumpWallVel; } set { jumpWallVel = value >= 0 ? value : jumpWallVelBase; } }
    private int multiJump;
    public int MultiJump { get { return multiJump; } set { multiJump = value >= 0 ? value : multiJump; } }
    private float diveVel;
    public float DiveVel { get { return diveVel; } set { diveVel = value >= 0 ? value : diveVelBase; } }

    // Editor Props
    [SerializeField]
    private float walkGroundSpeedBase = 10; // OK
    [SerializeField]
    private float jumpGroundVelBase = 15; // OK
    [SerializeField]
    private float jumpWallVelBase = 10; // OK
    [SerializeField]
    private float diveVelBase = 40; // OK
    [SerializeField]
    private float wallDetachCooldown = 1; // OK
    [SerializeField]
    private float groundInertiaBase = 0; // OK
    [SerializeField]
    private float airInertiaBase = 0; // OK
    [SerializeField]
    private int multiJumpBase = 1; // OK
    [SerializeField]
    private bool isSticky = false; // TODO

    // private fields
    PlayerCondition playerCondition;
    private AnimationManager animationManager;
    private LifeManager lifeManager;

    new void Start()
    {
        base.Start();

        body = GetComponent<Rigidbody2D>();
        Debug.Assert(body != null, "could not find player collider");

        collider = GetComponent<Collider2D>();
        Debug.Assert(collider != null, "could not find player collider");

        input = GetComponent<InputRouter>();
        Debug.Assert(collider != null, "could not find input router");

        playerCondition = GetComponent<PlayerCondition>();
        Debug.Assert(playerCondition != null, "could not find playerCondition");

        animationManager = GetComponent<AnimationManager>();
        Debug.Assert(animationManager != null, "could not find animationManager");

        lifeManager = GetComponent<LifeManager>();
        Debug.Assert(lifeManager != null, "could not find lifeManager");

        Physics2D.gravity = Vector2.zero;

        WallDetachState = WallDetach.CONTROL;
        Rebase();
    }

    public void Rebase()
    {
        GroundInertia = groundInertiaBase;
        AirInertia = airInertiaBase;
        WalkGroundSpeed = walkGroundSpeedBase;
        JumpGroundVel = jumpGroundVelBase;
        JumpWallVel = jumpWallVelBase;
        MultiJump = multiJumpBase;
        DiveVel = diveVelBase;
    }

    IEnumerator WallDetachTimer()
    {
        WallDetachState = WallDetach.ATTACHED;
        yield return new WaitForSeconds(wallDetachCooldown);
        WallDetachState = WallDetach.DETACHED;
    }


    Vector3 Horizontal(Vector3 vel)
    {
        var hor = input.X;
        var newVel = Vector3.zero;
        if (ForcedInput != null)
        {
            // Debug.Log("FX" + ForcedInput.X);
            hor = ForcedInput.X;
        }


        // Debug.Log(hor);
        if (IsGrounded && !IsWalled && !playerCondition.Handicaps[(int)PlayerCondition.Handicap.MOVE])
        {
            // newVel = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * new Vector3(WalkGroundSpeed * hor, 0, 0);
            var angle = Mathf.Rad2Deg * Mathf.Atan2(Normal.y, Normal.x) - 90;
            Debug.DrawLine(transform.position, transform.position + new Vector3(Normal.x, Normal.y, 0) * 10, Color.yellow);
            newVel = Quaternion.Euler(0, 0, angle) * new Vector3(WalkGroundSpeed * hor, 0, 0);
            // Debug.DrawLine(transform.position + new Vector3(0, 0.2f, 0), transform.position + new Vector3(0, 0.2f, 0) + new Vector3(vel.x, vel.y, 0), Color.red);
            Debug.DrawLine(transform.position, transform.position + new Vector3(newVel.x, newVel.y, 0), Color.white);
            var gi = GroundInertia < 0.8f ? 0f : 0.9f + GroundInertia * 0.1f;
            vel.x = newVel.x * (1 - gi) + vel.x * gi;

            if (Mathf.Abs(hor) < Mathf.Epsilon)
                animationManager.Movement = "idle";
            else
                animationManager.Movement = "run";
        }
        else if (!IsWalled && WallDetachState == WallDetach.CONTROL)
        {
            newVel = new Vector3(WalkGroundSpeed * hor, 0, 0);
            var ai = AirInertia < 0.8f ? 0f : 0.9f + AirInertia * 0.1f;
            // Debug.Log(vel.x + " " + newVel.x);
            vel.x = newVel.x * (1 - ai) + vel.x * ai;
            animationManager.Movement = "air";
        }
        else if (!IsWalled && WallDetachState == WallDetach.DETACHED)
        {
            if (Mathf.Abs(hor) > Mathf.Epsilon)
            {
                WallDetachState = WallDetach.CONTROL;
            }
            animationManager.Movement = "air";
        }

        body.AddForce(gravity);
        if (IsWalled) 
        {
            animationManager.Movement = "wall";
            vel.y = Mathf.Max(0, vel.y);
        }
            
        
        vel.y += ExtraVel.y;
        vel.x += ExtraVel.x;
        ExtraVel *= 0.9f; // FIXME

        return vel;
    }

    private void ScaleXForce(float hor) 
    {
        transform.localScale = new Vector3(
            Mathf.Abs(transform.localScale.x) * hor > 0 ? 1 : -1,
            transform.localScale.y,
            transform.localScale.z);
    }

    Vector3 Jump(Vector3 vel)
    {
        // JUMP
        if (!playerCondition.Handicaps[(int)PlayerCondition.Handicap.MOVE])
        {
            if (WallCollider != null)
            {
                ColliderDistance2D distance2D = collider.Distance(WallCollider);
                if (input.J && !prevJ)
                {
                    var velx = -distance2D.normal.x * JumpWallVel * (distance2D.isOverlapped ? 1 : -1);
                    vel.y = JumpGroundVel;
                    vel.x += velx;
                    ScaleXForce(velx);
                    StartCoroutine(WallDetachTimer());
                    jumpLeft = multiJump - 1; // FIXME
                    animationManager.Command = "jump";
                }
            }
            else
            {
                if ((IsGrounded || jumpLeft > 0) && (input.J && !prevJ))
                {
                    if (IsGrounded)
                    {
                        jumpLeft = MultiJump; // FIXME
                        vel.y = JumpGroundVel;
                    }
                    else
                        vel.y = JumpGroundVel;
                    jumpLeft--;
                    animationManager.Command = "jump";
                }
            }
        }
        return vel;
    }

    Vector3 Dive(Vector3 vel)
    {
        if (!playerCondition.Handicaps[(int)PlayerCondition.Handicap.MOVE])
        {
            if (!IsGrounded)
            {
                if (input.Y < -0.5f)
                {
                    vel.y = Mathf.Min(vel.y, -DiveVel);
                }
            }
        }
        return vel;
    }

    void FixedUpdate()
    {
        if (lifeManager.Life <= 0) return;

        var vel = body.velocity;

   
        ScaleX();
        vel = Horizontal(vel);
        vel = Jump(vel);
        vel = Dive(vel);

        // string runningAnim = "";
        // skeletonAnimation?.state?.Tracks.ForEach(t => runningAnim = t.Animation.Name);
            
        prevJ = input.J;
        //Debug.Log(IsGrounded + "/" + IsWalled + "/" + IsWallDetached);
        // Debug.Log(jumpLeft);
        body.velocity = vel;
    }
}
