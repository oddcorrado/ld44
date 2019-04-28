using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag : MonoBehaviour
{
    public int size = 3;
    public Text gemText;
    public UiGems uiGems;
    private List<string> gems = new List<string>();
    private LifeManager lifeManager;

    // Start is called before the first frame update
    void Start()
    {
        lifeManager = GetComponent<LifeManager>();
    }

    public bool AddGem(string name)
    {
        if (gems.Count >= size) return false;
        gems.Add(name);

        uiGems.UpdateGems(gems);


        return true;
    }

    public void EmptyBag(Dictionary<string,int> rates)
    {
        int totalCure = 0;
        gems.RemoveAll(gem =>
        {
            int cure = 0;
            rates.TryGetValue(gem, out cure);
            totalCure += cure;
            return true;
        });

        lifeManager.Life = Mathf.Min(lifeManager.Life + totalCure, lifeManager.lifeMax);
    }
}
