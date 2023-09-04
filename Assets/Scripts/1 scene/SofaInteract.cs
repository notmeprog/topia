using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class SofaInteract : MonoBehaviour, IInteractable
{
    public GameObject[] highlightObj;

    [Header("Cameras")]
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject cutsceneCamera;

    [Header("Dialog")]
    [SerializeField] private GameObject canvasDialog;
    [SerializeField] private MMFeedbacks enterFeedback;
    [SerializeField] private Animator mainAudioAnim;
    //[SerializeField] AudioSource mainClip;
    //[SerializeField] AudioSource speechPart;

    bool interact = false;


    public void Highlight()
    {
        for (int i = 0; i < highlightObj.Length; i++)
            highlightObj[i].layer = LayerMask.NameToLayer("Interactable");
    }

    public void Interact()
    {
        interact = true;

        cutsceneCamera.SetActive(true);
        mainCamera.SetActive(false);

        StartCoroutine("StartRadioSpeech");
    }

    IEnumerator StartRadioSpeech()
    {
        yield return new WaitForSeconds(4);
        enterFeedback?.PlayFeedbacks();
        mainAudioAnim.SetTrigger("MusicDown");
        yield return new WaitForSeconds(2);
        canvasDialog.SetActive(true);
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

        cutsceneCamera.SetActive(false);
        mainCamera.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && interact)
        {
            AfterInteract();
        }
    }
}
