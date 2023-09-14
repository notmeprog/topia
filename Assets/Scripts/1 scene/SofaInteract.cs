using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SofaInteract : MonoBehaviour, IInteractable
{
    public GameObject[] highlightObj;

    [Header("Cameras")]
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject cutsceneCamera;

    [Header("Dialog")]
    [SerializeField] private DialogTypewrite dialogTypewriteSc;
    [SerializeField] private GameObject canvasDialog;
    [SerializeField] private MMFeedbacks enterFeedback;
    [SerializeField] private Animator mainAudioAnim;

    [Header("ChangeMusic")]
    [SerializeField] private MMFeedbacks switchFeedback;
    [SerializeField] private AudioSource mainAudio;
    [SerializeField] private AudioClip audioClip;

    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("TextTip")]
    [SerializeField] private Animator textExit;

    [Header("Distortion")]
    public Volume volume;
    LensDistortion lensDistortion;

    public AnimationCurve curveStart; // Кривая изменения искажения
    public float startDuration = 5.0f; // Продолжительность анимации
    [Space(10)]
    public AnimationCurve curveEnd; // Кривая изменения искажения
    public float endDuration = 2.0f; // Продолжительность анимации
    bool oneTime = false;
    //[SerializeField] AudioSource mainClip;
    //[SerializeField] AudioSource speechPart;

    bool interact = false;

    void Start()
    {
        if (volume.profile.TryGet(out LensDistortion distortion))
        {
            lensDistortion = distortion;
        }
    }


    public void Highlight()
    {
        for (int i = 0; i < highlightObj.Length; i++)
            highlightObj[i].layer = LayerMask.NameToLayer("Interactable");
    }

    public void Interact()
    {
        interact = true;

        textExit.gameObject.SetActive(true);

        cutsceneCamera.SetActive(true);
        mainCamera.SetActive(false);

        if (!DifferentStatic.isRadioSofaPlaying)
            StartCoroutine("StartRadioSpeech");
    }

    IEnumerator StartRadioSpeech()
    {
        DifferentStatic.isRadioSofaPlaying = true;
        yield return new WaitForSeconds(4);
        enterFeedback?.PlayFeedbacks();
        mainAudioAnim.SetTrigger("MusicDown");
        yield return new WaitForSeconds(2);
        canvasDialog.SetActive(true);
    }

    IEnumerator DistortionStart()
    {
        float startTime = Time.time;
        float endTime = startTime + startDuration;

        while (Time.time < endTime)
        {
            float timeSinceStart = Time.time - startTime;
            float normalizedTime = timeSinceStart / startDuration;

            float distortionValue = curveStart.Evaluate(normalizedTime);

            lensDistortion.intensity.Override(distortionValue);

            yield return null;
        }

        // Убедитесь, что значение параметра устанавливается в конечное значение после завершения анимации
        lensDistortion.intensity.Override(curveStart.Evaluate(1.0f));
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (dialogTypewriteSc.endDialog && !oneTime && distance < 15)
        {
            oneTime = true;

            switchFeedback?.PlayFeedbacks();
            mainAudio.clip = audioClip;
            mainAudio.Play();

            //StartCoroutine("DistortionStart");
        }

        if (DifferentStatic.isRadioSofaPlaying && distance >= 15)
        {
            canvasDialog.SetActive(false);
        }

        if (Input.GetKey(KeyCode.Escape) && interact)
        {
            AfterInteract();
        }
    }







    ///////////////////////////////////////////////////////////////////////////////////////////////////

    /*IEnumerator PlaySpeech()
    {
        yield return new WaitForSeconds(6);
        mainClip.volume /= 3;
        speechPart.Play();

        yield return new WaitForSeconds(39);
        mainClip.volume *= 3;
    }*/

    public void AfterInteract()
    {
        interact = false;

        textExit.SetTrigger("Out");

        cutsceneCamera.SetActive(false);
        mainCamera.SetActive(true);

        mainAudioAnim.SetTrigger("MusicUp");

        //StartCoroutine("DistortionEnd");
    }

    IEnumerator DistortionEnd()
    {
        yield return new WaitForSeconds(5);

        float startTime = Time.time;
        float endTime = startTime + endDuration;

        while (Time.time < endTime)
        {
            float timeSinceStart = Time.time - startTime;
            float normalizedTime = timeSinceStart / endDuration;

            float distortionValue = curveEnd.Evaluate(normalizedTime);

            lensDistortion.intensity.Override(distortionValue);

            yield return null;
        }

        // Убедитесь, что значение параметра устанавливается в конечное значение после завершения анимации
        lensDistortion.intensity.Override(curveEnd.Evaluate(1.0f));
    }
}
