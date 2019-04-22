using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputRouter))]
public class PlayerLife : LifeManager {
    public override int Life
    {
        get { return life; }
        set
        {
            life = value;
            UiPlayerFinder.Instance.SetLife(input.PlayerId, value);
        }
    }

    [SerializeField]
    private int lifeBase = 100;
    [SerializeField]
    private bool isDeactivate = false;

    private InputRouter input;

    void Start()
    {
        input = GetComponent<InputRouter>();
        Life = lifeBase;
    }

    public override void Damage(int value, int playerId)
    {
        if(playerId != input.PlayerId)
            Life -= value;
        
        if (Life <= 0)
        {
            if (isDeactivate) gameObject.SetActive(false);
            else Destroy(gameObject);
        }
    }

    public override void Cure(int value, int playerId)
    {
        if (playerId == input.PlayerId)
            Life += value;
    }
}
