using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class RiffleInteract : MonoBehaviour, IInteractable
{
    public GameObject[] highlightObj;

    [SerializeField] Transform weaponsParent;

    [SerializeField] Transform realWeapon;

    [Header("PickupEffect")]
    [SerializeField] MMFeedbacks pickupEffect;
    [SerializeField] float speedOfPickup = 2;
    [SerializeField] Animator blinkEyesAnim;

    [Header("Croco")]
    [SerializeField] CutsceneCroco cutsceneCroco;

    bool startMove = false;


    public void Highlight()
    {
        for (int i = 0; i < highlightObj.Length; i++)
            highlightObj[i].layer = LayerMask.NameToLayer("Interactable");
    }

    public void Interact()
    {
        transform.SetParent(weaponsParent);

        startMove = true;

        Invoke("PickupEffect", 1);
        blinkEyesAnim.SetTrigger("BlinkEyes");

        cutsceneCroco.SetCutscene();
    }

    void PickupEffect()
    {
        pickupEffect?.PlayFeedbacks();
        CameraShake.Instance.ShakeCamera(3f, 0.2f, 1);

        DifferentStatic.isWeaponPickup = true;

        realWeapon.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void AfterInteract()
    {
        //
    }

    void Update()
    {
        if (startMove)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, speedOfPickup * Time.deltaTime);

            transform.localRotation = Quaternion.Lerp(transform.localRotation, realWeapon.localRotation, speedOfPickup * Time.deltaTime);
        }
    }
}
