using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(InputRouter))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerMana))]
public class Command : MonoBehaviour 
{
    public enum Condition { Grounded, Air}
    [SerializeField]
    protected InputRouter.Key[] keys;
    [SerializeField]
    protected Condition[] conditions;
    [SerializeField]
    protected int cost;
    [SerializeField]
    protected string preAnimation;
    [SerializeField]
    protected string postAnimation;
    [SerializeField]
    protected float cooldown;
    [SerializeField]
    protected bool cancelable = true;
    [SerializeField]
    protected bool continuous = false;


    static public Dictionary<int, Command> runningCommands = new Dictionary<int, Command>();

    protected InputRouter input;
    protected PlayerMovement playerMovement;
    protected PlayerMana playerMana;
    protected int id = 0;
    private float dateReady = 0;
    private bool prevInputCheck = false;
    private int playerId;
    private AnimationManager animationManager;

    // Use this for initialization
	protected void Start ()
    {
        input = GetComponent<InputRouter>();
        playerId = input.PlayerId;
        playerMovement = GetComponent<PlayerMovement>();
        playerMana = GetComponent<PlayerMana>();
        animationManager = GetComponent<AnimationManager>();
	}
	
    protected bool CheckConditions()
    {
        foreach(var condition in conditions)
        {
            switch(condition)
            {
                case Condition.Air:
                    if (playerMovement.IsGrounded) return false;
                    break;
                case Condition.Grounded:
                        if (!playerMovement.IsGrounded) return false;
                    break;
                default:
                    break;
            }
        }
        return true;    
    }

    protected bool CheckCost()
    {
        if (playerMana.Mana < cost) return false;
        return true;
    }

    protected bool CheckCooldown()
    {
        if (Time.time < dateReady) return false;
        return true;
    }

    protected bool CheckInput()
    {
        var inputCheck = input.Check(keys);
        if (continuous)
        {
            if (inputCheck == false) return false;
        }
        else
        {
            // keep this order !
            if (prevInputCheck && inputCheck) return false;
            prevInputCheck = inputCheck;
            if (inputCheck == false) return false;
        }
        return true;
    }

    protected bool CheckRunningCommands()
    {
        Command runningCommand;
        runningCommands.TryGetValue(playerId, out runningCommand);
        if (runningCommand != null)
        {
            if (!runningCommand.cancelable) return false;
            runningCommand.Stop();
            runningCommands.Remove(playerId);
        }
        runningCommands.Add(playerId, this);
        return true;
    }

    protected void ClearRunningCommand()
    {
        Command runningCommand;
        runningCommands.TryGetValue(playerId, out runningCommand);
        if (runningCommand != null)
        {
            runningCommands.Remove(playerId);
            animationManager.Command = null;
        }
            
    }

    protected void UpdateCost()
    {
        playerMana.Mana -= cost;
    }

    protected void UpdateCooldown()
    {
        dateReady = Time.time + cooldown;
    }

    public virtual void Stop()
    {
        Debug.LogWarning("Stop not implemented");   
    }

    protected bool Check()
    {
        if (CheckInput() == false) return false;
        if (CheckConditions() == false) return false;
        if (CheckCost() == false) return false;
        if (CheckCooldown() == false) return false;
        if (CheckRunningCommands() == false) return false;

        UpdateCost();
        UpdateCooldown();
        animationManager.Command = preAnimation;
        return true;
    }

}
