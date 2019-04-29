using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StockExchange : MonoBehaviour
{
    public AnimationCurve red = new AnimationCurve(new Keyframe[3] 
        { 
            new Keyframe(0, 0),
            new Keyframe(5, 30),
            new Keyframe(10, 0)
        }
    );
    public AnimationCurve green = new AnimationCurve(new Keyframe[3]
        {
                new Keyframe(0, 30),
                new Keyframe(5, 0),
                new Keyframe(10, 30)
        }
    );
    public AnimationCurve blue = new AnimationCurve(new Keyframe[3]
        {
                    new Keyframe(0, 5),
                    new Keyframe(5, 15),
                    new Keyframe(10, 5)
        }
    );

    public AnimationCurve yellow = new AnimationCurve(new Keyframe[3]
        {
                        new Keyframe(0, 10),
                        new Keyframe(5, 10),
                        new Keyframe(10, 10)
        }
    );


    public float Red {
        get {
            return red.Evaluate(Time.time % red.keys[red.length - 1].time);
        }
    }

    public float Green
    {
        get
        {
            return green.Evaluate(Time.time % green.keys[green.length - 1].time);
        }
    }

    public float Blue
    {
        get
        {
            return blue.Evaluate(Time.time % blue.keys[blue.length - 1].time);
        }
    }

    public float Yellow
    {
        get
        {
            return yellow.Evaluate(Time.time % yellow.keys[yellow.length - 1].time);
        }
    }

    public Text textRed;
    public Text textBlue;
    public Text textYellow;
    public Text textGreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textRed.text = "red :" + (int)Red;
        textBlue.text = "blue :" + (int)Blue;
        textYellow.text = "yellow :" + (int)Yellow;
        textGreen.text = "green :" + (int)Green;
    }
}
