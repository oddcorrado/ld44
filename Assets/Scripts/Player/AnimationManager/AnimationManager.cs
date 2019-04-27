using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    protected string movement = null;
    public virtual string Movement
    {
        get { return movement; }
        set
        {
            Debug.Log("fck " + value);
            movement = value;
        }
    }

    protected string command = null;
    public virtual string Command
    {
        get { return command; }
        set
        {
            Debug.Log("fck " + value);
            if (command == value) return;
            command = value;
        }
    }

    protected string handicap = null;
    public virtual string Handicap
    {
        get { return handicap; }
        set
        {
            Debug.Log("fck " + value);
            handicap = value;
        }
    }
}
