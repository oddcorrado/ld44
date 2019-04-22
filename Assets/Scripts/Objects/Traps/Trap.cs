using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Trap : PlayerObject
{
    [SerializeField]
    private int maxCount = 3;
    [SerializeField]
    private bool dieOnTrigger = false;
    [SerializeField]
    private int enterDamage = 0;
    [SerializeField]
    private int stayDamage = 0;
    [SerializeField]
    private int duration = 0;

    // privates
    private Collider2D coll;
    private float endDate;

    void OnTriggerEnter2D(Collider2D other)
	{
        var inputRouter = other?.gameObject?.GetComponent<InputRouter>();
        if (inputRouter && inputRouter.PlayerId == PlayerId) return;

        if(enterDamage > 0)
        {
            var life = other?.gameObject?.GetComponent<LifeManager>();

            if (life) life.Damage(enterDamage, PlayerId);
        }

        if(dieOnTrigger)
        {
            gameObject.SetActive(false);
        }
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
        // FIXME playerid

        if (stayDamage > 0)
        {
            var life = collision?.gameObject?.GetComponent<LifeManager>();
            if (life) life.Damage(stayDamage, PlayerId);
        }
	}

	void Update()
	{
        if (duration != 0 && Time.time > endDate) gameObject.SetActive(false);
	}
}
