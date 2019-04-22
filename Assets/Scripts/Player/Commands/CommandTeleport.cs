using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandTeleport : Command
{
    [SerializeField]
    private float distance = 3;
    [SerializeField]
    private GameObject teleport;

    private Coroutine coroutine;

    IEnumerator Teleport()
    {
        teleport.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        teleport.SetActive(false);
        coroutine = null;
    }

    void Update()
    {
        if (!Check()) return;

        var dir = new Vector3(input.X, input.Y, 0).normalized;
        var position = transform.position + dir * distance;

        position.x = Mathf.Clamp(position.x, FightManager.Instance.Bounds.xMin, FightManager.Instance.Bounds.xMax);
        position.y = Mathf.Clamp(position.y, FightManager.Instance.Bounds.yMin, FightManager.Instance.Bounds.yMax);
        transform.position = position;

        var angle = Mathf.Rad2Deg * Mathf.Atan2(input.Y, input.X);
        teleport.transform.rotation = Quaternion.Euler(0, 0, angle);
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(Teleport());
    }
}
