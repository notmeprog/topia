using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class BlurEyes : MonoBehaviour
{
    [SerializeField] MMFeedbacks blinkEyesFeedback;

    private void OnEnable()
    {
        blinkEyesFeedback?.PlayFeedbacks();
    }
}
