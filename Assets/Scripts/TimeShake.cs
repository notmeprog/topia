using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeShake : MonoBehaviour
{
    public float delay = 1;
    public float timerShake = .1f;
    public float strength = 3;
    void Start()
    {
        Invoke("ShakeIt", delay);
    }

    void ShakeIt()
    {
        CameraShake.Instance.ShakeCamera(strength, timerShake, 1);
    }
}
