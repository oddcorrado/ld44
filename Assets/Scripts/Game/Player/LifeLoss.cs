using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeLoss : MonoBehaviour
{
    public PlayerLife playerLife;

    private float lifeLossSpeed = 0.01f;
    public float LifeLossSpeed
    {
        get { return lifeLossSpeed;  }
        set { lifeLossSpeed = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerLife.Life = playerLife.Life - LifeLossSpeed;
    }
}
