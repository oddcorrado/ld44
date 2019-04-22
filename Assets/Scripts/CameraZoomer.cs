using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomer : MonoBehaviour
{

    new private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //
        float aspectRatio = (float)16 / (float)9;
        float topY = 7.2f;
        float topX = aspectRatio * topY;

        if (FightManager.Instance.Players.Length <= 0) return;

        Vector3 pos = Vector3.zero;

        float maxX = -Mathf.Infinity;
        float maxY = -Mathf.Infinity;
        float minX = Mathf.Infinity;
        float minY = Mathf.Infinity;
        float ratio = 1 / (float)FightManager.Instance.Players.Length;
        foreach(var player in FightManager.Instance.Players)
        {
            maxX = Mathf.Max(maxX, player.gameObject.transform.position.x);
            maxY = Mathf.Max(maxY, player.gameObject.transform.position.y);
            minX = Mathf.Min(minX, player.gameObject.transform.position.x);
            minY = Mathf.Min(minY, player.gameObject.transform.position.y);
            pos += player.gameObject.transform.position;
        }

        pos *= ratio;

        float size = Mathf.Min(topY, Mathf.Max(2, 0.7f * Mathf.Max(maxY - minY, (maxX - minX) / aspectRatio)));
        pos.x = Mathf.Clamp(pos.x, -topX + aspectRatio * size, topX - aspectRatio * size);
        pos.y = Mathf.Clamp(pos.y, -topY + size, topY - size);

        var deltaSize = size - camera.orthographicSize;

        if(deltaSize < 0)
        {
            deltaSize = Mathf.Max(deltaSize, -0.01f);
        }
        else
        {
            deltaSize = Mathf.Min(deltaSize, 0.1f);
        }
        camera.orthographicSize += deltaSize;

        var deltaPos = new Vector3(pos.x, pos.y, -100) - transform.position;
        Debug.Log(deltaPos.magnitude);
        if(deltaPos.magnitude > 0.1f)
        transform.position += Mathf.Min(0.1f, deltaPos.magnitude) * deltaPos.normalized;
    }
}
