using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour

{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        LifeManager lm = go.GetComponent<LifeManager>();

        if (lm == null) return;
        lm.Life = lm.Life - damage;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.collider.gameObject;
        LifeManager lm = go.GetComponent<LifeManager>();

        if (lm == null) return;
        lm.Life = lm.Life - damage;
 
    }
}
