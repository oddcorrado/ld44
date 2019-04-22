using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerCondition))]
public class CommandDiveBlast : Command
{
    [SerializeField]
    private PooledBullet prefab;
    [SerializeField]
    private float velocity = 10;

    private PlayerCondition playerCondition;
    private Rigidbody2D body;
    private bool isWaitingCrash = false;

    new void Start()
	{
        base.Start();
        playerCondition = GetComponent<PlayerCondition>();
        body = GetComponent<Rigidbody2D>();
	}

	private void Update()
    {
        if (!Check()) return;

        body.velocity = new Vector3(0, -velocity, 0);
        /* playerCondition.AddCondition(new PlayerCondition.Condition()
        {
            originator = gameObject,
            handicap = PlayerCondition.Handicap.MOVE,
            type = PlayerCondition.Type.DIVE,
            endDate = Time.time + 10,
            priority = 10,
            retrigger = PlayerCondition.Retrigger.IGNORE
        }); */
        isWaitingCrash = true;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if(isWaitingCrash)
        {
            isWaitingCrash = false;
            playerCondition.AddCondition(new PlayerCondition.Condition()
            {
                originator = gameObject,
                handicap = PlayerCondition.Handicap.MOVE,
                type = PlayerCondition.Type.DIVERECOVER,
                endDate = Time.time + 2,
                cancels = new PlayerCondition.Type[] {PlayerCondition.Type.DIVE},
                priority = 10,
                retrigger = PlayerCondition.Retrigger.IGNORE
            });
            PooledBullet blast = prefab.Get<PooledBullet>(true);

            blast.transform.position = transform.position;
            body.velocity = Vector3.zero;
            blast.GetComponent<PlayerObject>().PlayerId = input.PlayerId;
        }
	}
}