using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandPunchAnim : CommandPunch
{
    [SerializeField]
    AnimationCurve punchX = new AnimationCurve(new Keyframe[2]{new Keyframe(0, 0), new Keyframe(0.5f, 1)});
    [SerializeField]
    AnimationCurve punchY = new AnimationCurve(new Keyframe[2] { new Keyframe(0, 0), new Keyframe(0.5f, 1)});
    [SerializeField]
    private bool ForcedInput;
    [SerializeField]
    AnimationCurve ForcedInputX = new AnimationCurve(new Keyframe[2] { new Keyframe(0, 3), new Keyframe(0.5f, 0) });

    Coroutine coroutine;

    IEnumerator timeoutCoroutineEnumerator()
    {
        float startDate = Time.time;
        punch.SetActive(true);
        while(Time.time < startDate + duration)
        {
            float date = Time.time - startDate;
            yield return new WaitForSeconds(0.016f);
            float x = punchX.Evaluate(date % punchX.keys[punchX.length - 1].time);
            float y = punchY.Evaluate(date % punchY.keys[punchY.length - 1].time);
            punch.transform.localPosition = new Vector2(x, y);

            if (ForcedInput)
            {
                float fx = ForcedInputX.Evaluate(date % ForcedInputX.keys[ForcedInputX.length - 1].time);
                fx *= transform.localScale.x;
                playerMovement.ForcedInput = new PlayerMovement.Input()
                {
                    X = fx,
                    Y = 0,
                    J = 0
                };
            }
            else
                playerMovement.ForcedInput = null;
        }
        punch.SetActive(false);
        playerMovement.ForcedInput = null;
        ClearRunningCommand();
        coroutine = null;
    }

    public override void Hit(GameObject go)
    {
        var life = go.GetComponent<PlayerLife>();
        if (life != null)
        {
            life.Damage(damage, input.PlayerId);
            go.GetComponent<PlayerCondition>().AddCondition(new PlayerCondition.Condition()
            {
                handicap = PlayerCondition.Handicap.MOVE,
                type = PlayerCondition.Type.STUN,
                endDate = Time.time + 3
            });
            var body = go.GetComponent<Rigidbody2D>();
            if (body != null)
            {

                body.velocity = (pushForce * (go.transform.position - transform.position).normalized);
            }
        }
    }

    public override void Stop()
    {
        if (coroutine != null)
        {
            punch.SetActive(false);
            StopCoroutine(coroutine);
        }
        playerMovement.ForcedInput = null;
        coroutine = null;
    }

    private void Update()
    {
        if (!Check()) return;

        if(coroutine == null) {
            coroutine = StartCoroutine(timeoutCoroutineEnumerator());
        }
        // StopAllCoroutines();

    }
}
