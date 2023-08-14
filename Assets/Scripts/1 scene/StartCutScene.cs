using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class StartCutScene : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] PlayerMovementAdvanced playerMovementAdvanced;

    [Header("ChangeView")]
    [SerializeField] private Transform mainCinemaCam;
    [SerializeField] private Transform cameraView;
    [SerializeField] private AudioSource audioSource;

    [Header("KinoEffect")]
    [SerializeField] GameObject canvasKino;

    [SerializeField] Animator imageUpAnim;
    [SerializeField] Animator imageDownAnim;

    [Header("New Color")]
    [SerializeField] Volume volume;
    ColorAdjustments colorAdjustments;
    [SerializeField] int newHueShift;
    bool changeColor = false;

    [Header("UiToClose")]
    [SerializeField] private GameObject[] closedUI;

    void Start()
    {
        StartCoroutine("ChangeView");
    }

    // Update is called once per frame
    void Update()
    {
        if (changeColor)
            colorAdjustments.hueShift.value = Mathf.Lerp(colorAdjustments.hueShift.value, newHueShift, 1 * Time.deltaTime);
        else
            colorAdjustments.hueShift.value = Mathf.Lerp(colorAdjustments.hueShift.value, 0, 1 * Time.deltaTime);
    }

    IEnumerator ChangeView()
    {
        yield return new WaitForSeconds(4);
        audioSource.Play();
        yield return new WaitForSeconds(2);
        playerMovementAdvanced.stopMoving = true;

        canvasKino.SetActive(true);
        changeColor = true;

        for (int i = 0; i < closedUI.Length; i++)
            closedUI[i].SetActive(false);

        cameraView.position = new Vector3(mainCinemaCam.position.x, mainCinemaCam.position.y, mainCinemaCam.position.z);
        mainCinemaCam.gameObject.SetActive(false);
        cameraView.gameObject.SetActive(true);


        yield return new WaitForSeconds(3);
        cameraView.gameObject.SetActive(false);

        changeColor = false;
        imageDownAnim.SetTrigger("Close");
        imageUpAnim.SetTrigger("Close");

        for (int i = 0; i < closedUI.Length; i++)
            closedUI[i].SetActive(true);

        mainCinemaCam.gameObject.SetActive(true);

        playerMovementAdvanced.stopMoving = false;
    }
}
