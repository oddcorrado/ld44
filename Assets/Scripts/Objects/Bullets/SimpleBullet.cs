using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleBullet : PlayerObject {
    [SerializeField]
    protected int damage = 1;
    [SerializeField]
    protected bool orient = true;
    [SerializeField]
    protected bool mirror = false;

    protected Rigidbody2D body;
    new protected ParticleSystem particleSystem;
    new protected Collider2D collider;
    SkeletonAnimation skeletonAnimation;
    protected Coroutine dieCoroutine;

    protected void Start()
    {
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        particleSystem = transform?.Find("Particles").GetComponent<ParticleSystem>();
        skeletonAnimation = transform?.Find("Spine").GetComponent<SkeletonAnimation>();
    }


    protected IEnumerator Die()
    {
        if (particleSystem != null) particleSystem.Stop();
        if (skeletonAnimation != null) skeletonAnimation.state.SetAnimation(0, "die", false);
        yield return new WaitForSeconds(2);
        collider.isTrigger = false;
        gameObject.SetActive(false);
        if (skeletonAnimation != null) skeletonAnimation.state.SetAnimation(0, "fly", true);
        if (particleSystem != null) particleSystem.Play();
        dieCoroutine = null;
    }

    protected virtual void OnEnable()
	{
        if (collider != null) collider.isTrigger = false;
        if (skeletonAnimation != null) skeletonAnimation.state.SetAnimation(0, "fly", true);
        if (particleSystem != null) particleSystem.Play();
	}

	protected virtual void OnCollisionEnter2D(Collision2D collision)
	{
        var life = collision?.collider?.gameObject.GetComponent<LifeManager>();
        collider.isTrigger = true;
        if(life != null)
        {
            life.Damage(damage, PlayerId);
        }

        var po = collision?.collider?.gameObject.GetComponent<PlayerObject>();
        // if (po == null)//  || po.PlayerId != PlayerId)
        dieCoroutine = StartCoroutine(Die());
       // else
         //   return; // FIXME
	}

    protected void Update()
	{
        if(orient)
        {
            var vel = body.velocity;
            var angle = Mathf.Rad2Deg * Mathf.Atan2(vel.y, vel.x);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        if(mirror)
        {
            if (body.velocity.x < 0)
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y));
            else
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y));
        }
	}
}
