using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class TriggerHome : MonoBehaviour
{
    TimeFreezer timeFreezerSc;
    [SerializeField] MMFeedbacks audioFeedback;

    void Start()
    {
        timeFreezerSc = GetComponent<TimeFreezer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !DifferentStatic.isTriggerHomeEnter)
        {
            DifferentStatic.isTriggerHomeEnter = true;

            audioFeedback?.PlayFeedbacks();
            timeFreezerSc.FreezeTime(.2f);
        }
    }
}
