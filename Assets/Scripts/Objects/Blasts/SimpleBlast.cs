using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SimpleBlast : PlayerObject
{
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float startRatio = 1;
    [SerializeField]
    private float endRatio = 3;
    [SerializeField]
    private float duration = 2;


    protected Collider2D coll;
    private List<GameObject> blasted = new List<GameObject>();
    private float endDate;
    private Vector2 size;

    protected void Start()
    {
        coll = GetComponent<Collider2D>();
        size = transform.localScale;
    }

    public void OnEnable()
    {
        endDate = Time.time + duration;
        blasted.RemoveAll(p => true);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var go = collider?.gameObject;
        Debug.Log("trig " + go);
        if (blasted.Find(g => g == go)) return;

        var po = collider?.gameObject.GetComponent<PlayerObject>();
        Debug.Log("trig2 " + po);
        if (po != null && po.PlayerId == PlayerId) return;

        var pl = go.GetComponent<PlayerLife>();
        Debug.Log("trig2 " + pl);
        if (pl != null) pl.Damage(damage, PlayerId);

        blasted.Add(go);
    }


    protected void Update()
    {
        if(Time.time > endDate) gameObject.SetActive(false);
        var ratio = (endDate - Time.time) / duration;
        transform.localScale = size * (startRatio * ratio + endRatio * (1 - ratio));
    }
}