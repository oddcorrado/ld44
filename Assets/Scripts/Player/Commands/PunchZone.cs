using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchZone : MonoBehaviour {
    private CommandPunch commandPunch;

    void Start()
    {
        commandPunch = transform.parent.GetComponent<CommandPunch>();    
    }

	void OnTriggerEnter2D(Collider2D collision)
	{
        commandPunch.Hit(collision.gameObject);
	}
}
