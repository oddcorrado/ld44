using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour {
    static private FightManager instance;
    static public FightManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<FightManager>();
                Debug.Assert(instance != null, "cannot find fight manager");
            }
            return instance;
        }
    }

    [System.Serializable]
    public class Player
    {
        public int playerId;
        public GameObject prefab;
        public GameObject gameObject;
    }
    [SerializeField]
    private Player[] players;
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private Rect bounds;
    public Rect Bounds { get { return bounds; }}

    public Player[] Players { get { return players; }}

	void Start ()
    {
        if(PersistentData.SelectedFighters.Count == 0)
            UseEditorFighters();
        
        players = new Player[PersistentData.SelectedFighters.Count];
        for (int i = 0; i < PersistentData.SelectedFighters.Count; i++)
        {
            var f = PersistentData.SelectedFighters[i];
            if(f.prefab != null)
            {
                var go = Instantiate(f.prefab);
                go.transform.position = spawnPoints[i].position;
                go.GetComponent<InputRouter>().PlayerId = f.playerId;
                players[i] = new Player()
                {
                    playerId = f.playerId,
                    prefab = f.prefab,
                    gameObject = go
                };
                // TODO spawn point
            }
        }
	}
	
    void UseEditorFighters()
    {
        PersistentData.SelectedFighters.RemoveAll(f => true);
        for (int i = 0; i < players.Length; i++)
        {
            var p = players[i];
            var f = new PersistentData.Fighter()
            {
                name = p.prefab.name,
                prefab = p.prefab,
                playerId = p.playerId
            };
            PersistentData.SelectedFighters.Add(f);
        }
    }
	void Update () 
    {
        int living = players.Length;
        int winnerId = 0;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null || players[i].gameObject == null) living--; else winnerId = i + 1;
        }
            
        if (living == 1) Debug.Log("winner is player " + winnerId);
	}
}
