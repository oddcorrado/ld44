using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerMovement : MonoBehaviour {
    // Privates
    protected Rigidbody2D body;
    protected new Collider2D collider;
    protected InputRouter input;
    protected readonly Vector3 gravity = new Vector2(0, -20);
    protected int jumpLeft = 1;
    protected bool prevJ = false;
    protected Coroutine stunCoroutine = null;
    protected Coroutine animationCoroutine = null;
    protected SkeletonAnimation skeletonAnimation;

	protected void Start()
	{
        skeletonAnimation = transform?.Find("Spine").GetComponent<SkeletonAnimation>();
	}

	public class Input
    {
        public float X;
        public float Y;
        public float J;
    }

    // enums
    public enum WallDetach {ATTACHED, DETACHED, CONTROL}

    // Properties
    public bool IsGrounded { get; set; }
    public bool IsWalled { get; set; }
    public WallDetach WallDetachState { get; set; }
    public bool IsStunned { get; set; }
    public Collider2D WallCollider { get; set; }
    public Vector3 Normal { get; set; }
    public Vector2 ExtraVel { get; set; }
    public Input ForcedInput = null;

    protected void ScaleX()
    {
        var hor = input.X;
        // SCALE
        if (!IsStunned && Mathf.Abs(hor) > Mathf.Epsilon)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x) * hor > 0 ? 1 : -1,
                transform.localScale.y,
                transform.localScale.z);
        }
    }

    IEnumerator StunCoroutine(float duration)
    {
        IsStunned = true;
        yield return new WaitForSeconds(duration);
        IsStunned = false;
    }

    public void Stun(float duration)
    {
        if (stunCoroutine != null) StopCoroutine(stunCoroutine);
        stunCoroutine = StartCoroutine(StunCoroutine(duration));
    }

    protected IEnumerator SpineOneShot(string now, string next, bool loop)
    {
        skeletonAnimation.state.ClearTrack(0);
        var track = skeletonAnimation.state.SetAnimation(0, now, false);
        yield return new WaitForSpineAnimationComplete(track);
        skeletonAnimation.state.SetAnimation(0, next, loop);
        animationCoroutine = null;
    }

    protected IEnumerator SpineOneShot(string now)
    {
        var loop = skeletonAnimation.state.GetCurrent(0).Loop;
        var next = skeletonAnimation.state.GetCurrent(0).Animation.Name;
        return SpineOneShot(now, next, loop);
    }
}
