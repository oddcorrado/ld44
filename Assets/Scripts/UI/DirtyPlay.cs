using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DirtyPlay : MonoBehaviour
{
    [SerializeField]
    private DirtyFighterSelect[] fighterSelects;
    [SerializeField]
    private string[] levels;
    [SerializeField]
    private GameObject[] prefabs;

    public void Play()
    {
        PersistentData.SelectedFighters.RemoveAll(f => true);
        foreach(DirtyFighterSelect fs in fighterSelects)
        {
            if(prefabs[fs.Index] != null) {
                PersistentData.SelectedFighters.Add(new PersistentData.Fighter()
                {
                    playerId = fs.PlayerId,
                    prefab = prefabs[fs.Index],
                    name = prefabs[fs.Index]?.name
                });
            }
            else
            {
                PersistentData.SelectedFighters.Add(new PersistentData.Fighter()
                {
                    playerId = fs.PlayerId,
                    prefab = null,
                    name = "none"
                });
            }

        }
        PersistentData.SelectedFighters.ForEach(sf =>
        {
            // Debug.Log(sf.playerId + " " + sf.prefab + " " + sf.name);
        });
        string levelName = levels[PersistentData.Level];
        SceneManager.LoadScene(levelName);
    }
}
