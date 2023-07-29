using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallShake : MonoBehaviour
{
    void Start()
    {
        CameraShake.Instance.ShakeCamera(.5f, 1.1f, 1);
    }
}
