using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiGems : MonoBehaviour
{
    public Text displayText;

    public void UpdateGems(List<string> gems)
    {
        string gemString = "";
        gems.ForEach(gem =>
        {
            gemString = gemString + gem;
        });

        displayText.text = gemString;
    }
}
