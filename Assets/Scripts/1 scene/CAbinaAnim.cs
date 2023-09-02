using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class CAbinaAnim : MonoBehaviour
{
    [Header("CabinaAnim")]
    [SerializeField] private AudioSource rotateAudio;
    [SerializeField] private AudioSource openDoorsAudio;
    [SerializeField] private ParticleSystem openParticle;
    [SerializeField] private AudioSource actuallyOpenAudio;
    [SerializeField] private AudioClip windSFX;
    [SerializeField] MMFeedbacks windFeedback;

    public void PlaySoundRotate()
    {
        rotateAudio.Play();
        CameraShake.Instance.ShakeCamera(2f, .1f, 1);
    }

    public void OpenDoors()
    {
        openDoorsAudio.Play();
        CameraShake.Instance.ShakeCamera(1f, .5f, 1);
    }

    public void ActuallyOpen()
    {
        actuallyOpenAudio.Play();
        CameraShake.Instance.ShakeCamera(5f, .1f, 1);
        openParticle.Play();
    }

    public void AfterOpen()
    {
        actuallyOpenAudio.clip = windSFX;
        actuallyOpenAudio.volume = .7f;
        actuallyOpenAudio.pitch = 1;
        actuallyOpenAudio.Play();
        windFeedback?.PlayFeedbacks();
    }
}
