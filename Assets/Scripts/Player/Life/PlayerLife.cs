﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputRouter))]
public class PlayerLife : LifeManager {
    public override float Life
    {
        get { return life; }
        set
        {
            if (life - value > 2)
            {
                bloodFx.Play(); 
            }
                
            if (life - value < -2)
            {
                cureFx.Play();
            }

            life = value;
            LifeText.text = ((int)life).ToString();

            if (Life <= 0)
            {
                GetComponent<AnimationManager>().Handicap = "die";
                gameOverPanel.SetActive(true);
                // if (isDeactivate) gameObject.SetActive(false);
                // else Destroy(gameObject);
            }

            //UiPlayerFinder.Instance.SetLife(input.PlayerId, (int) value);
        }
    }

    [SerializeField]
    private int lifeBase = 100;
    [SerializeField]
    private bool isDeactivate = false;
    [SerializeField]
    private ParticleSystem bloodFx;
    [SerializeField]
    private ParticleSystem cureFx;

    public Text LifeText;
    public GameObject gameOverPanel;

    private InputRouter input;

    void Start()
    {
        input = GetComponent<InputRouter>();
        Life = lifeBase;
        bloodFx.Stop(); 
        cureFx.Stop(); 
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
