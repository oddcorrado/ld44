using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputRouter))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerMana : MonoBehaviour
{
    [SerializeField]
    private bool airLoad = true;

    private int mana;
    public int Mana
    { 
        get { return mana; }
        set 
        {
            mana = value;
            UiPlayerFinder.Instance.SetMana(input.PlayerId, value);
        } 
    }

    [SerializeField]
    private int manaBase = 100;

    private InputRouter input;
    private PlayerMovement playerMovement;

	void Start()
	{
        input = GetComponent<InputRouter>();
        playerMovement = GetComponent<PlayerMovement>();
        Mana = manaBase;
	}

	void Update()
	{
        if (!airLoad && !playerMovement.IsGrounded && !playerMovement.IsWalled) return;
        Mana = Mathf.Min(manaBase, Mana + 1);	
	}
}
