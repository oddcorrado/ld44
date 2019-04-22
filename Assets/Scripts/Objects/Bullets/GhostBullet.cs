using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBullet : SimpleBullet
{
    [SerializeField]
    private float duration = 2;

    private float endDate;

    new protected void OnEnable()
    {
        base.OnEnable();
        endDate = Time.time + duration;
        if(collider != null) collider.isTrigger = true;
    }

    new protected void Update()
    {
        base.Update();
        if(dieCoroutine == null && Time.time > endDate) dieCoroutine = StartCoroutine(Die());
    }

    new void OnCollisionEnter2D(Collision2D collision)
    {
    }

	protected void OnTriggerEnter2D(Collider2D collider)
	{
        var po = collider?.gameObject.GetComponent<PlayerObject>();
        var ir = collider?.gameObject.GetComponent<InputRouter>();
        if ((po == null || po.PlayerId == PlayerId) && (ir == null || ir.PlayerId == PlayerId)) return;

        var life = collider?.gameObject.GetComponent<LifeManager>();
        if (life != null) life.Damage(damage, PlayerId);
	}
}
