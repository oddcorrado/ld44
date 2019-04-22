using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputRouter))]
public class CommandShootAim : Command
{
    [Serializable]
    public class AngleRange
    {
        public float min;
        public float max;
    }
    [SerializeField]
    private PooledBullet prefab;
    [SerializeField]
    private float distance;
    [SerializeField]
    private float velocity;
    [SerializeField]
    private AngleRange[] angleRanges;
    private Transform target;

    private Vector2 dir;

    new void Start()
    {
        base.Start();

        foreach (var range in angleRanges)
        {
            if (range.min > range.max
                || Mathf.Abs(range.min) > 360
                || range.min < 0
                || Mathf.Abs(range.max) > 360
                || range.max < 0)
                Debug.LogError("unvalid range " + range.min + "/" + range.max);
        }
    }

    private Vector2 RestrainAngle()
    {
        if (angleRanges.Length == 0)
            return dir;


        float angle = Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x);
        if (angle < 0) angle += 360;
        float best = 2000;

        foreach (var range in angleRanges)
        {
            if (angle >= range.min && angle <= range.max) return dir;
            float clamp = Mathf.Clamp(angle, range.min, range.max);

            if (Mathf.Abs(angle - clamp) < Mathf.Abs(angle - best)) best = clamp;
        }

        return Quaternion.Euler(0, 0, best) * Vector2.right;
    }

    void Place(PooledBullet bullet)
    {
        bullet.transform.position = transform.position + distance * new Vector3(dir.x, dir.y, 0).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = dir * velocity;
    }

    void FindClosestTarget()
    {
        target = null;
        foreach(var p in FightManager.Instance.Players)
        {
            if(p.playerId != input.PlayerId && p.gameObject != null)
            {
                if (target == null)
                    target = p.gameObject.transform;
                else if ((transform.position - target.position).magnitude > (transform.position - p.gameObject.transform.position).magnitude)
                    target = p.gameObject.transform;
            }
        }
    }

    void Update()
    {
        if (!Check()) return;

        PooledBullet bullet = prefab.Get<PooledBullet>(true);

        FindClosestTarget();
        if(target != null)
        {
            dir = (target.position - transform.position).normalized;
            dir = RestrainAngle();
            Place(bullet);
        }
    }
}
