using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTracker : MonoBehaviour
{
    public Transform[] tracked;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float minX, minY, maxX, maxY;
        minX = minY = maxX = maxY = 0.0f;

        if(tracked.Length != 0)
        {
            minX = maxX = tracked[0].position.x;
            minY = maxY = tracked[0].position.y;
        }
        if(tracked.Length > 1)
        {
            minX = maxX = tracked[0].position.x;
            minY = maxY = tracked[0].position.y;
            for (int i = 1; i < tracked.Length; i++)
            {
                Vector3 pos = tracked[i].position;
                if (pos.x < minX) minX = pos.x;
                if (pos.x > maxX) maxX = pos.x;
                if (pos.y < minY) minY = pos.y;
                if (pos.y > maxY) maxY = pos.y;
            }
        }

        Vector3 finalPos = new Vector3();
        finalPos.x = (maxX + minX) / 2.0f;
        finalPos.y = (maxY + minY) / 2.0f;
        if (Mathf.Abs(maxX - minX) > Mathf.Abs(maxY - minY))
            finalPos.z = Mathf.Abs(maxX - minX) * -1;
        else
            finalPos.z = Mathf.Abs(maxY - minY) * -1;

        transform.position = finalPos + offset;
    }
}
