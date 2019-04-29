using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public GameObject up;
    public GameObject down;

    public void Market(bool isUp)
    {
        up.SetActive(isUp);
        down.SetActive(!isUp);
    }
}
