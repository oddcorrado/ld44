using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GroundTester : MonoBehaviour
{
    private readonly List<Collider2D> others = new List<Collider2D>();
    private PlayerMovement playerMovement;

	void Start()
	{
        playerMovement = transform.parent.gameObject.GetComponent<PlayerMovement>();
        Debug.Assert(playerMovement != null, "could not find player controller");
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        others.Add(other);
        playerMovement.IsGrounded = others.Count > 0;
	}

    void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Break();
        others.Remove(other);
        playerMovement.IsGrounded =others.Count > 0;
    }

	private void Update()
	{
        string sum = "";
        others.RemoveAll(o => o == null);
        others.ForEach(o => sum += " " + o.gameObject.name);
        playerMovement.IsGrounded = others.Count > 0;
        // Debug.Log(others.Count + sum);
    }
}
