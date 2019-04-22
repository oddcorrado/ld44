using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyFighterSelect : MonoBehaviour
{
    [SerializeField]
    private int playerId;
    public int PlayerId { get { return playerId; }}

    public int Index { get; set; }

	public void Select(int index)
	{
        Debug.Log(index);
        Index = index;
	}
}
