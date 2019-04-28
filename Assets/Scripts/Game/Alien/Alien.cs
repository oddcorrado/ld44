using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    [System.Serializable]
    public class Cure 
    {
        public int value;
        public string name;
    }

    public Cure[] cures;
    private Dictionary<string, int> rates = new Dictionary<string, int>();

    void Start()
    {
        foreach(Cure cure in cures)
        {
            rates.Add(cure.name, cure.value);
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        Bag bag = collision.gameObject.GetComponent<Bag>();

        if (bag == null) return;

        bag.EmptyBag(rates);
	}
}
