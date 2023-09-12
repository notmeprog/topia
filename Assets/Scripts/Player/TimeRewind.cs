using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewind : MonoBehaviour
{
    public KeyCode rewindButton;
    public float timeRewind;

    bool isRewind;
    public List<Vector3> positionList;
    void Start()
    {
        positionList = new List<Vector3>();
    }

    private void FixedUpdate()
    {
        if (isRewind)
        {
            if (positionList.Count > 0)
            {
                int lastPosition = positionList.Count - 1;
                transform.position = positionList[lastPosition];
                positionList.RemoveAt(lastPosition);
            }
        }
        else
        {
            if (positionList.Count >= timeRewind / Time.fixedDeltaTime)
            {
                positionList.RemoveAt(0);
            }
            positionList.Add(transform.position);
        }
    }
}
