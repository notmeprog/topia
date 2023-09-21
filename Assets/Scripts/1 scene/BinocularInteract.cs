using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinocularInteract : MonoBehaviour, IInteractable
{
    public GameObject[] highlightObj;

    public FreeLookCamera freeLookCamera;
    Vector2 cameraRot;

    [Header("Cameras")]
    GameObject mainCamera;
    [SerializeField] GameObject binocularCamera;
    [SerializeField] GameObject binocularUI;
    [SerializeField] GameObject owl;
    Animator binocularUIAnim;

    [Header("SFX")]
    [SerializeField] AudioClip cameraOpen;
    [SerializeField] AudioClip cameraClose;
    AudioSource audioSource;

    [Header("Player")]
    PlayerMovementAdvanced playerMovementAdvanced;

    bool interact = false;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("CMcam1");
        playerMovementAdvanced = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementAdvanced>();

        audioSource = GetComponent<AudioSource>();
        cameraRot = freeLookCamera.CameraRot;
        binocularUIAnim = binocularUI.GetComponent<Animator>();
    }


    public void Highlight()
    {
        for (int i = 0; i < highlightObj.Length; i++)
            highlightObj[i].layer = LayerMask.NameToLayer("Interactable");
    }

    public void Interact()
    {
        Invoke("SetInteract", 1.5f);

        binocularUI.SetActive(true);
        binocularCamera.SetActive(true);
        mainCamera.SetActive(false);

        DifferentStatic.canOpenPauseMenu = false;
        playerMovementAdvanced.stopMoving = true;

        if (!DifferentStatic.playerSeeOwl)
            owl.SetActive(true);
    }

    public void AfterInteract()
    {
        interact = false;
        audioSource.PlayOneShot(cameraClose);

        binocularUIAnim.SetTrigger("Close");
        Invoke("CloseUI", 3f);
        Invoke("CloseCamera", 1.5f);
    }

    void CloseUI()
    {
        binocularUI.SetActive(false);
    }

    void CloseCamera()
    {
        binocularCamera.SetActive(false);
        mainCamera.SetActive(true);

        owl.SetActive(false);

        DifferentStatic.canOpenPauseMenu = true;
        playerMovementAdvanced.stopMoving = false;
    }

    void SetInteract()
    {
        interact = true;

        audioSource.PlayOneShot(cameraOpen);
    }

    void Update()
    {
        if (cameraRot != freeLookCamera.CameraRot && interact)
        {
            cameraRot = freeLookCamera.CameraRot;

            CameraShake.Instance.ShakeCamera(.2f, 0.1f, 1);
        }

        if (Input.GetKey(KeyCode.Escape) && interact)
        {
            AfterInteract();
        }
    }
}
