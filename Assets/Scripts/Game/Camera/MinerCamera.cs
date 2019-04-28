using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerCamera : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, 0, -126);
        Debug.Log("player " + player.transform.position.x);
    }
}
