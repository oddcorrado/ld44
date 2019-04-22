using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public enum Type { FROZEN, STUN, DIVE, DIVERECOVER }
    public enum Retrigger { IGNORE, OVERWRITE }
    public enum Handicap { MOVE, ATTACK, MANA }
    public class Condition
    {
        public Type type;
        public Handicap handicap;
        public float endDate;
        public Retrigger retrigger;
        public int priority;
        public GameObject originator;
        public int damage;
        public Type[] cancels;
        public InputRouter.Key[] reducers;
        public float reduceAmount;
    }

    private List<Condition> conditions = new List<Condition>();

    private Condition main;
    public Condition Main { get { return main; } }
    private bool[] handicaps = new bool[System.Enum.GetNames(typeof(Handicap)).Length];
    public bool[] Handicaps { get { return handicaps; } set { handicaps = value; } }
    [SerializeField]
    private GameObject[] visualizers = new GameObject[System.Enum.GetNames(typeof(Type)).Length];

    void Update()
    {
        conditions.ForEach(c =>
        {
            if(c.endDate < Time.time)
            {
                int vindex = (int)c.type;
                if (visualizers[vindex] != null)
                {
                    visualizers[vindex].SetActive(false);
                }
            }

        });

        conditions.RemoveAll(c => c.endDate < Time.time);
        main = null;
        for (int i = 0; i < Handicaps.Length; i++) Handicaps[i] = false;
        conditions.ForEach(c =>
        {
            Handicaps[(int)c.handicap] = true;
            if (main == null || c.priority > main.priority) main = c;
        });
    }

    public void AddCondition(Condition condition)
    {
        Condition similar = conditions.Find(c => c.type == condition.type);
        if(similar != null)
        {
            if (condition.retrigger == Retrigger.IGNORE) return;
            if (condition.retrigger == Retrigger.OVERWRITE)
            {
                conditions.Remove(similar);
                conditions.Add(condition);
                return;
            }
        }

        conditions.Add(condition);
        int vindex = (int)condition.type;
        if (visualizers[vindex] != null)
        {
            visualizers[vindex].SetActive(true);
        }
        Handicaps[(int)condition.handicap] = true;
    }
}
