using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScene : MonoBehaviour
{
    public string name;

	public void Play()
	{
        SceneManager.LoadScene(name);
	}
}
