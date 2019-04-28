using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
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
        Bag bag = go.GetComponent<Bag>();

        if (bag == null) return;

        if (bag.AddGem("bidon")) Destroy(gameObject);
    }
}
