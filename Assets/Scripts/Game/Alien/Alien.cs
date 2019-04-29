using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alien : MonoBehaviour
{
    [System.Serializable]
    public class Cure 
    {
        public int value;
        public string name;
    }

    public Cure[] cures;
    public int gemTotal = 10;
    public Text gemText;
    public GameObject successPanel;

    private int gemCount = 0;

    private Dictionary<string, int> rates = new Dictionary<string, int>();

    void Start()
    {
        foreach(Cure cure in cures)
        {
            rates.Add(cure.name, cure.value);
        }
        gemText.text = "gems:" + gemCount + "/" + gemTotal;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        Bag bag = collision.gameObject.GetComponent<Bag>();

        if (bag == null) return;

        gemCount += bag.EmptyBag(rates);

        gemText.text = "gems: " + gemCount + "/" + gemTotal;

        if (gemCount >= gemTotal) successPanel.SetActive(true);
	}
}
