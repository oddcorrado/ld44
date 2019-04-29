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

    [SerializeField]
    private ParticleSystem gemFx;

    // Start is called before the first frame update
    void Start()
    {
        lifeManager = GetComponent<LifeManager>();
        gemFx.Stop();
    }

    public bool AddGem(string name)
    {
        if (gems.Count >= size) return false;
        gems.Add(name);

        uiGems.UpdateGems(gems);
        gemFx.Play();

        return true;
    }

    public int EmptyBag(Dictionary<string,int> rates)
    {
        int totalCure = 0;
        int count = gems.Count;
        gems.RemoveAll(gem =>
        {
            int cure = 0;
            rates.TryGetValue(gem, out cure);
            totalCure += cure;
            return true;
        });

        lifeManager.Life = Mathf.Min(lifeManager.Life + totalCure, lifeManager.lifeMax);
        uiGems.UpdateGems(gems);
        return count;
    }
}
