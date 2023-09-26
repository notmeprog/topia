using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using TMPro;

public class EndDemo : MonoBehaviour
{
    [SerializeField] private GameObject uiEnd;
    [SerializeField] private GameObject backText;
    [SerializeField] private TextMeshProUGUI[] textLogo;
    [SerializeField] private Color mainColor;
    [SerializeField] private Color shadowColor;
    [SerializeField] private GameObject endDialog;
    [SerializeField] private Animator ambientForest;

    [Header("Effects")]
    [SerializeField] MMFeedbacks endFeedback;
    [SerializeField] private Animator mainAudioAnim;
    [SerializeField] private AudioSource headBoom;

    [Header("Dialog")]
    [SerializeField] private DialogTypewrite dialogTypewriteSc;
    bool oneTime = false;


    public void OpenEndCanvas()
    {
        DifferentStatic.canOpenPauseMenu = false;

        uiEnd.SetActive(true);
        ambientForest.SetTrigger("End");

        StartCoroutine("OpenDialog");
    }

    IEnumerator OpenDialog()
    {
        mainAudioAnim.SetTrigger("PitchDownDeath");
        yield return new WaitForSeconds(3);
        for (int i = 0; i < textLogo.Length; i++)
            textLogo[i].text = "<bounce>rowtropia</bounce>";

        textLogo[0].color = mainColor;
        textLogo[1].color = shadowColor;

        CameraShake.Instance.ShakeCamera(4f, 0.1f, 1);
        endFeedback?.PlayFeedbacks();
        yield return new WaitForSeconds(.5f);
        backText.SetActive(true);
        yield return new WaitForSeconds(7);
        endDialog.SetActive(true);
    }

    void Update()
    {
        if (dialogTypewriteSc.endDialog && !oneTime)
        {
            oneTime = true;

            headBoom.GetComponent<Animator>().enabled = true;
            headBoom.Play();

            Invoke("ExitGame", 2);
        }
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
