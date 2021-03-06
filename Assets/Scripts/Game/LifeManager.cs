﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public int lifeMax = 100;

    protected float life;
    public virtual float Life
    {
        get { return life; }
        set
        {
            life = value;
        }
    }

    void Start()
    {
    }

    public virtual void Damage(int value, int playerId)
    {
    }

    public virtual void Cure(int value, int playerId)
    {
    }
}
