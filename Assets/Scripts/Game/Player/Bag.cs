using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public int size = 3;
    private List<string> gems = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddGem(string name)
    {
        if (gems.Count >= size) return false;
        gems.Add(name);
        return true;
    }

    public void EmptyBag()
    {
        gems.RemoveAll(gem =>
        {
            return true;
        });
    }
}
