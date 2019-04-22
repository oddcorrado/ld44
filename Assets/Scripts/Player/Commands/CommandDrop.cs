using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputRouter))]
public class CommandDrop : Command
{
    [SerializeField]
    private PooledBullet prefab = null;
    [SerializeField]
    private Transform dropTransform = null;
    [SerializeField]
    private int maxCount = 3;


    static List<GameObject> objects = new List<GameObject>();

    void MaxOut(GameObject go)
    {
        objects.RemoveAll(g => !g.activeSelf);
        while (objects.Count >= maxCount)
        {

            var toRemove = objects[0];
            objects.RemoveAt(0);
            toRemove.SetActive(false);
            Debug.Log("remove " + toRemove.name);
        }
        objects.Add(go);
    }

    void Place(PooledBullet bullet)
    {
        bullet.transform.position = dropTransform.position;
        bullet.GetComponent<PlayerObject>().PlayerId = input.PlayerId;
        MaxOut(bullet.gameObject);
    }

    void Update()
    {
        if (!Check()) return;

        PooledBullet bullet = prefab.Get<PooledBullet>(true);
        Debug.Log("place " + bullet.name);
        Place(bullet);
    }
}
