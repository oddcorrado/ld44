using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Totem))]
public class TotemLife : LifeManager
{
    public override float Life
    {
        get { return life; }
        set
        {
            life = value;
        }
    }

    [SerializeField]
    private int lifeBase = 10;

    private Totem totem;

    void Start()
    {
        totem = GetComponent<Totem>();
        Life = lifeBase;
    }

    public override void Damage(int value, int playerId)
    {
        if (playerId != totem.PlayerId)
            Life -= value;

        if (Life <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public override void Cure(int value, int playerId)
    {
        if (playerId == totem.PlayerId)
            Life += value;
    }
}
