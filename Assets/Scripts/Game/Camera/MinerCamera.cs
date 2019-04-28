using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerCamera : MonoBehaviour
{
    public GameObject player;
    public float offset=3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
        if(player.transform.position.x-transform.position.x<-offset)
        {
            transform.position = new Vector3(player.transform.position.x+offset, 0, -126);
            Debug.Log("player " + player.transform.position.x);

        }
        if(player.transform.position.x-transform.position.x>offset)
        {
            transform.position = new Vector3(player.transform.position.x-offset, 0, -126);
            Debug.Log("player " + player.transform.position.x);
        }
     
     }
}
