using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alien : MonoBehaviour
{
    public int gemTotal = 10;
    public Text gemText;
    public GameObject successPanel;
    public int unlocks = 0;
    public StockExchange stockExchange; 

    private int gemCount = 0;

    private Dictionary<string, int> rates = new Dictionary<string, int>();

    void Start()
    {
        gemText.text = "gems: " + gemCount + "/" + gemTotal;
    }

    private Dictionary<string, int> BuildRates()
    {
        Dictionary<string, int> output = new Dictionary<string, int>
        {
            { "red", (int)stockExchange.Red },
            { "yellow", (int)stockExchange.Yellow },
            { "blue", (int)stockExchange.Blue },
            { "green", (int)stockExchange.Green }
        };

        return output;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        Bag bag = collision.gameObject.GetComponent<Bag>();

        if (bag == null) return;

        gemCount += bag.EmptyBag(BuildRates());

        gemText.text = "gems: " + gemCount + "/" + gemTotal;

        if (gemCount >= gemTotal)
        {
            successPanel.SetActive(true);
            int best = PlayerPrefs.GetInt("level");
            if (best < unlocks)
                PlayerPrefs.SetInt("level", unlocks);
        }
	}
}
