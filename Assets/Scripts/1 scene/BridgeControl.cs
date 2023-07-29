using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class BridgeControl : MonoBehaviour
{
    [Header("Effect")]
    [SerializeField] GameObject dustParticle;
    [SerializeField] Transform pointParticle;

    [SerializeField] MMFeedbacks bridgeFeedback;
    AudioSource audioSource;

    [Header("Animator")]
    [SerializeField] Animator bridgeAnim;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartShake()
    {
        CameraShake.Instance.ShakeCamera(.3f, 1.25f, 1);
        audioSource.Play();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
            bridgeAnim.SetTrigger("bridgeDown");
    }

    public void AnimEffect()
    {
        Instantiate(dustParticle, pointParticle.position, Quaternion.identity);
        CameraShake.Instance.ShakeCamera(4f, 0.2f, 1);
        bridgeFeedback?.PlayFeedbacks();
    }
}
