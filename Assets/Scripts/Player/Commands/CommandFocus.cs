using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LifeManager))]
public class CommandFocus : Command
{
    [SerializeField]
    private FocusFill focusFill;
    [SerializeField]
    private FocusExecute focusExecute;
    [SerializeField]
    private float duration = 3;

    enum State { IDLE, FOCUS }
    private int prevLife;
    private LifeManager lifeManager;
    private State state;
    private float fill = 0;

    new void Start()
	{
        base.Start();
        lifeManager = GetComponent<LifeManager>();
	}

	private void Reset()
	{
        state = State.IDLE;
        fill = 0;
        focusFill.SetFill(0);
        focusFill.Display(false);
        ClearRunningCommand();
	}

	void Update()
    {
        if (state == State.IDLE && Check())
        {
            focusFill.Display(true);
            state = State.FOCUS;
        }

        if (state == State.IDLE)
        {
            return;
        }

        if (lifeManager.Life < prevLife)
        {
            Reset();
            return;;
        }

        if (Mathf.Abs(input.X) > Mathf.Epsilon )
        {

            focusFill.Display(false);
            state = State.IDLE;
            ClearRunningCommand();
            return;
        }

        // Debug.Log("FOCUS " + (Time.time - startDate) / duration);
        fill += Time.deltaTime / duration;
        focusFill.SetFill(fill);

        if(fill > 1)
        {
            // state = State.IDLE;
            // startDate = Time.time;
            fill = 0;
            focusExecute.Execute();
            // Debug.Log("Focus complete");
        }
    }
}
