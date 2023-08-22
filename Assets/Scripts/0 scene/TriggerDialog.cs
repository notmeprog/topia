using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TriggerDialog : MonoBehaviour
{
    [SerializeField] private PlayerMovementAdvanced playerMovementAdvancedSc;
    [SerializeField] private GameObject canvasDialog;

    [Header("Two cameras")]
    [SerializeField] private GameObject mainCam;
    [SerializeField] private GameObject cam2;

    [Header("Distortion")]
    public Volume volume;
    LensDistortion lensDistortion;

    public AnimationCurve distortionCurve; // Кривая изменения искажения
    public float animationDuration = 2.0f; // Продолжительность анимации

    [Header("PitchAudio")]
    [SerializeField] private Animator pitchEmbient;

    [Header("После главного диалога")]
    [SerializeField] private DialogTypewrite mainDialogSc;
    [SerializeField] Animator blinkEyesAnim;
    bool twoTime = false;


    void Start()
    {
        if (volume.profile.TryGet(out LensDistortion distortion))
        {
            lensDistortion = distortion;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("StartDialog");

            StartCoroutine(ChangeDistortion());
        }
    }

    IEnumerator ChangeDistortion()
    {
        float startTime = Time.time;
        float endTime = startTime + animationDuration;

        while (Time.time < endTime)
        {
            float timeSinceStart = Time.time - startTime;
            float normalizedTime = timeSinceStart / animationDuration;

            float distortionValue = distortionCurve.Evaluate(normalizedTime);

            lensDistortion.intensity.Override(distortionValue);

            //print(distortionValue);

            yield return null;
        }

        // Убедитесь, что значение параметра устанавливается в конечное значение после завершения анимации
        lensDistortion.intensity.Override(distortionCurve.Evaluate(1.0f));
    }

    IEnumerator StartDialog()
    {
        pitchEmbient.SetTrigger("PitchUp");

        playerMovementAdvancedSc.stopMoving = true;

        //mainCam.SetActive(false);
        cam2.SetActive(true);

        yield return new WaitForSeconds(1);
        canvasDialog.SetActive(true);
    }

    void Update()
    {
        if (mainDialogSc.endDialog && !twoTime)
        {
            twoTime = true;

            StartCoroutine("EndDialog");
        }
    }

    IEnumerator EndDialog()
    {
        cam2.GetComponent<Animator>().SetTrigger("EndDialog");

        yield return new WaitForSeconds(3);
        blinkEyesAnim.SetTrigger("CloseEyes");
        pitchEmbient.SetTrigger("PitchDown");
    }
}
