using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    CinemachineVirtualCamera virtualCamera;
    CinemachineBasicMultiChannelPerlin m_MultiChannelPerlin;

    float shakeTimer;
    float startIntensity;
    float shakeTimerTotal;

    void Awake()
    {
        Instance = this;

        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        m_MultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float timer, float frequency)
    {
        m_MultiChannelPerlin.m_FrequencyGain = frequency;
        m_MultiChannelPerlin.m_AmplitudeGain = intensity;
        startIntensity = intensity;
        shakeTimer = timer;
        shakeTimerTotal = timer;
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
                m_MultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startIntensity, 0, (1 - (shakeTimer / shakeTimerTotal)));
        }
    }
}
