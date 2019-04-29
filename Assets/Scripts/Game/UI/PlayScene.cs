using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayScene : MonoBehaviour
{
    public string name;
    public int minLevel = 0;

	public void Start()
	{
        Color color = new Color(1, 1, 1, 0.3f);

        if (PlayerPrefs.GetInt("level") < minLevel)
            GetComponent<Image>().color = color;
	}

	public void Play()
	{
        if (PlayerPrefs.GetInt("level") >= minLevel)
            SceneManager.LoadScene(name);
	}
}
