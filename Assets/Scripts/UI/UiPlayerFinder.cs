using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPlayerFinder : MonoBehaviour {
    static private UiPlayerFinder instance;
    static public UiPlayerFinder Instance
    { 
        get 
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UiPlayerFinder>();
                if(instance == null)
                {
                    var go = new GameObject();
                    go.AddComponent<UiPlayerFinder>();
                    instance = go.GetComponent<UiPlayerFinder>();
                    instance.Setup();
                }
            }
            return instance;
        }
    }
    protected class PlayerDisplay
    {
        public Text life;
        public Text mana;
    }

    private List<PlayerDisplay> playerDisplays = new List<PlayerDisplay>();

    void Setup()
    {
        int count = 2;
        for (int i = 1; i <= count; i++)
        {
            var life = GameObject.Find("Player" + i + "Life")?.GetComponent<Text>();
            var mana = GameObject.Find("Player" + i + "Mana")?.GetComponent<Text>();
            if(life != null && mana != null) playerDisplays.Add(new PlayerDisplay() { life = life, mana = mana });
        }
    }

    public void SetMana(int playerId, int mana)
    {
        if (playerId > playerDisplays.Count) return;
        playerDisplays[playerId - 1].mana.text = "P" + playerId + " MANNA: " + mana;
    }

    public void SetLife(int playerId, int life)
    {
        if (playerId > playerDisplays.Count) return;
        if(life <= 0) 
            playerDisplays[playerId - 1].mana.text = "P" + playerId + " MANNA: " + "DEAD";
        playerDisplays[playerId - 1].life.text = "P" + playerId + " LIFE: " + (life >= 0 ? life.ToString() : "DEAD");
    }
}
