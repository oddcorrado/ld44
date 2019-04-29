using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timeleft : MonoBehaviour
{
    public float timeleft = 60;
    public Text timeleftText;
    public GameObject successPanel;

    // Update is called once per frame
    void Update()
    {
        if (timeleft <= 0) return;

        timeleft -= Time.deltaTime;
        timeleftText.text = ((int) Mathf.Max(0, timeleft)).ToString() + " secs";
        if (timeleft <= 0)
        {
            if(GetComponent<LifeManager>().Life > 0)
            {
                successPanel.SetActive(true);
                GetComponent<LifeManager>().Life = 100;
            }
        }
    }
}
