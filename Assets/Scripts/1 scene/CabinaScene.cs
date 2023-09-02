using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class CabinaScene : MonoBehaviour
{
    [Header("Radio")]
    [SerializeField] private Animator radioAnim;
    [SerializeField] private GameObject radioAudio;
    [SerializeField] MMFeedbacks radioFeedback;
    [SerializeField] private GameObject dialog;

    [Header("FlipSkybox")]
    [SerializeField] private Material skyBoxMaterial;

    [Header("Player")]
    [SerializeField] private PlayerMovementAdvanced playerMovementAdvanced;

    [Header("DisableAfterDialog")]
    [SerializeField] private DialogTypewrite dialogTypewriteSc;
    [SerializeField] private GameObject radioParticle;

    [Header("Flash")]
    [SerializeField] private GameObject[] lightingParticle;
    [SerializeField] MMFeedbacks lightingFeedback;
    [SerializeField] GameObject panelFlesh;
    [SerializeField] private PlayerFirstCamera playerFirstCamera;
    [SerializeField] private GameObject[] disableObjs;
    [SerializeField] private GameObject[] destroyObjs;

    [Header("AfterFlash")]
    [SerializeField] private Animator mainAudioPitch;
    [SerializeField] private Color newFogColor;

    bool oneTime = false;


    void Start()
    {
        StartCoroutine("StartDialog");
    }

    IEnumerator StartDialog()
    {
        yield return new WaitForSeconds(4);
        radioAnim.enabled = true;
        radioFeedback?.PlayFeedbacks();
        radioAudio.SetActive(true);
        CameraShake.Instance.ShakeCamera(3f, 1f, 1);

        yield return new WaitForSeconds(3);
        dialog.SetActive(true);
    }

    void EndCabinScene()
    {
        playerMovementAdvanced.walkSpeed = 4.5f;
        playerMovementAdvanced.canCrouch = true;
    }

    public void ChangeSkybox()
    {
        RenderSettings.skybox = skyBoxMaterial;

        RenderSettings.fogColor = newFogColor;
    }

    private void Update()
    {
        if (dialogTypewriteSc.endDialog && !oneTime)
        {
            oneTime = true;
            radioAnim.enabled = false;
            radioParticle.SetActive(false);

            StartCoroutine("Flash");
        }
    }

    IEnumerator Flash()
    {
        yield return new WaitForSeconds(8);
        playerFirstCamera.sensX = 45;
        for (int i = 0; i < lightingParticle.Length; i++)
            lightingParticle[i].SetActive(true);

        lightingFeedback?.PlayFeedbacks();
        CameraShake.Instance.ShakeCamera(7f, 1f, 1);

        yield return new WaitForSeconds(1f);
        panelFlesh.SetActive(true);

        yield return new WaitForSeconds(3f);
        ChangeSkybox();
        for (int i = 0; i < disableObjs.Length; i++)
            disableObjs[i].SetActive(false);

        for (int i = 0; i < destroyObjs.Length; i++)
            Destroy(destroyObjs[i]);

        yield return new WaitForSeconds(7f);
        playerFirstCamera.sensX = 65;
        mainAudioPitch.enabled = true;
        mainAudioPitch.gameObject.GetComponent<AudioSource>().enabled = true;

    }


}
