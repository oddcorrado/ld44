using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class PersistentData
{
    public class Fighter
    {
        public GameObject prefab;
        public string name;
        public int playerId;
    }
    private static List<Fighter> selectedFighers = new List<Fighter>();
    public static List<Fighter> SelectedFighters { get {return selectedFighers;}}
    public static int Level { get; set; }
}