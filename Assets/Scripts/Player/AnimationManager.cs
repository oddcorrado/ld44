using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class AnimationManager : MonoBehaviour
{
    private string movement = null;
    public string Movement
    {
        get { return movement; }
        set
        {
            movement = value;
            CheckAnimation();
        }
    }

    private string command = null;
    public string Command
    {
        get { return command; }
        set 
        {
            if (command == value) return;
            command = value;
            // if (command != null) Debug.Log(gameObject.name + " anim " + command);
            if(command != null) animationCoroutine = StartCoroutine(SpineOneShot(command));
        }
    }

    private string handicap = null;
    public string Handicap
    {
        get { return handicap; }
        set
        {
            handicap = value;
            CheckAnimation();
        }
    }

    private SkeletonAnimation skeletonAnimation;
    private string runningAnimation;
    private Coroutine animationCoroutine;

    private IEnumerator SpineOneShot(string now)
    {
        if (animationCoroutine != null) StopCoroutine(animationCoroutine);

        // skeletonAnimation.state.ClearTrack(0);
        var track = skeletonAnimation.state.SetAnimation(0, now, false);
        runningAnimation = now;
        yield return new WaitForSpineAnimationComplete(track);
        Command = null;
        runningAnimation = null;
        CheckAnimation();
        animationCoroutine = null;
    }

    void Start()
    {
        skeletonAnimation = transform?.Find("Spine").GetComponent<SkeletonAnimation>();
        Debug.Assert(skeletonAnimation != null, "could not find skeletonAnimation");
    }

    private void CheckAnimation()
    {
        if (Command != null) return;

        if (Handicap != null && runningAnimation != Handicap)
        {
            runningAnimation = Handicap;
            // Debug.Log(gameObject.name + " anim " + Handicap);
            skeletonAnimation.state.SetAnimation(0, Handicap, true);
        }

        if (Movement != null && runningAnimation != Movement)
        {
            runningAnimation = Movement;
            // Debug.Log(gameObject.name + " anim " + Movement);
            skeletonAnimation.state.SetAnimation(0, Movement, true);
        }
    }

}
