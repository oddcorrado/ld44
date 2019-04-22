using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyLevelSelect : MonoBehaviour
{
	public void Select(int index)
	{
        PersistentData.Level = index;
	}
}
