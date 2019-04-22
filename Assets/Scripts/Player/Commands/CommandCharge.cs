using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandCharge : Command
{
    [SerializeField]
    private Collider2D chargeCollider;
    [SerializeField]
    private float speedBoost;
    [SerializeField]
    private float duration;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float pushForce;

    PlayerMovementGround playerMovementGround;
    private Coroutine coroutine;
    bool isCharging = false;

    protected new void Start()
    {
        base.Start();
        playerMovementGround = GetComponent<PlayerMovementGround>();
        Debug.Assert(playerMovementGround != null, "CommandCharge needs player movement ground");
    }

    IEnumerator ChargeTimeout()
    {
        var groundSpeed = playerMovementGround.WalkGroundSpeed;
        playerMovementGround.WalkGroundSpeed += speedBoost;
        isCharging = true;
        input.ForceMoveX = true;
        yield return new WaitForSeconds(duration);
        input.ForceMoveX = false;
        playerMovementGround.WalkGroundSpeed = groundSpeed;
        isCharging = false;
        coroutine = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isCharging) return;

        var go = collision?.collider?.gameObject;
        if (go == null) return;

        var life = go.GetComponent<PlayerLife>();
        if (life != null)
        {
            life.Damage(damage, input.PlayerId);
            go.GetComponent<PlayerCondition>().AddCondition(new PlayerCondition.Condition()
            {
                handicap = PlayerCondition.Handicap.MOVE,
                type = PlayerCondition.Type.STUN,
                endDate = Time.time + 3
            });
            var body = go.GetComponent<Rigidbody2D>();
            if (body != null)
            {

                body.velocity = (pushForce * (go.transform.position - transform.position).normalized);
            }
        }
    }

    public override void Stop()
    {
        base.Stop();
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    void Update()
    {
        if (!Check()) return;

        if (coroutine == null)
        {
            coroutine = StartCoroutine(ChargeTimeout());
        }

    }
}
