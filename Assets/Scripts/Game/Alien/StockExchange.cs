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

    public bool RedIsUp
    {
        get
        {
            return (red.Evaluate((Time.time + 1) % red.keys[red.length - 1].time) - red.Evaluate((Time.time) % red.keys[red.length - 1].time) > 0);
        }
    }

    public float Green
    {
        get
        {
            return green.Evaluate(Time.time % green.keys[green.length - 1].time);
        }
    }

    public bool GreenIsUp
    {
        get
        {
            return (green.Evaluate((Time.time + 1) % green.keys[green.length - 1].time) - green.Evaluate((Time.time) % green.keys[green.length - 1].time) > 0);
        }
    }

    public float Blue
    {
        get
        {
            return blue.Evaluate(Time.time % blue.keys[blue.length - 1].time);
        }
    }

    public bool BlueIsUp
    {
        get
        {
            return (blue.Evaluate((Time.time + 1) % blue.keys[blue.length - 1].time) - blue.Evaluate((Time.time) % blue.keys[blue.length - 1].time) > 0);
        }
    }

    public float Yellow
    {
        get
        {
            return yellow.Evaluate(Time.time % yellow.keys[yellow.length - 1].time);
        }
    }

    public bool YellowIsUp
    {
        get
        {
            return (yellow.Evaluate((Time.time + 1) % yellow.keys[yellow.length - 1].time) - yellow.Evaluate((Time.time) % yellow.keys[yellow.length - 1].time) > 0);
        }
    }

    public Text[] redTexts;
    public Text[] greenTexts;
    public Text[] blueTexts;
    public Text[] yellowTexts;

    public Indicator[] redIndicators;
    public Indicator[] greenIndicators;
    public Indicator[] blueIndicators;
    public Indicator[] yellowIndicators;

    public GameObject ticker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Text t in redTexts) t.text = "RED " + (int)Red;
        foreach (Text t in greenTexts) t.text = "GRN " + (int)Green;
        foreach (Text t in yellowTexts) t.text = "YLW " + (int)Yellow;
        foreach (Text t in blueTexts) t.text = "BLU " + (int)Blue;

        foreach (Indicator indicator in redIndicators) indicator.Market(RedIsUp);
        foreach (Indicator indicator in greenIndicators) indicator.Market(GreenIsUp);
        foreach (Indicator indicator in yellowIndicators) indicator.Market(YellowIsUp);
        foreach (Indicator indicator in blueIndicators) indicator.Market(BlueIsUp);


        var x = ticker.transform.position.x;
        x -= 2;
        x = x % 1600;
        ticker.transform.position = new Vector3(x, ticker.transform.position.y, ticker.transform.position.z);
    }
}
