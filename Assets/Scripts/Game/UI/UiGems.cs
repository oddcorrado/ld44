using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiGems : MonoBehaviour
{
    public Image[] images;

    public void UpdateGems(List<string> gems)
    {
        for (int i = 0; i < 3; i++)
        {
            images[i].color = new Color(0.1f, 0.1f, 0.1f);
        }

        for (int i = 0; i < gems.Count; i++)
        {
            switch(gems[i])
            {
                case "red":
                    images[i].color = new Color(1, 0 , 0);
                    break;
                case "green":
                    images[i].color = new Color(0, 1, 0);
                    break;
                case "yellow":
                    images[i].color = new Color(1, 1, 0);
                    break;
                case "blue":
                    images[i].color = new Color(0.2f, 0.2f, 1);
                    break;
                default:
                    images[i].color = new Color(0.1f, 0.1f, 0.1f);
                    break;

            }
        }
    }
}
