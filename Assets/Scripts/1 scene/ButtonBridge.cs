using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.Feedbacks;

public class ButtonBridge : MonoBehaviour
{
    //[SerializeField] Animator bridgeAnim;
    Animator buttonAnim;
    //[SerializeField] Renderer provod;
    [SerializeField] private Material provodMat;

    [Header("SecondButton")]
    [SerializeField] private SecondBridgeButton secondBridgeButtonSc;
    [SerializeField] private PressedBridge pressedBridgeSc;

    [Header("Color")]
    [SerializeField] Color newColor = Color.red;
    [SerializeField] Color standartColor;
    AudioSource audioSource;

    [Header("AudioClips")]
    [SerializeField] AudioClip buttonOn;
    [SerializeField] AudioClip buttonOff;


    public bool isPressed = false;

    bool inButton = false;


    void Start()
    {

        provodMat.SetColor("_EmissionColor", standartColor);

        buttonAnim = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        //provodMat = provod.material;
        //standartColor = provodMat.color;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.layer == 22 || collision.gameObject.tag == "Player") && !inButton)
        {
            isPressed = true;

            inButton = true;
            print(collision.gameObject.name);

            //bridgeAnim.SetTrigger("bridgeDown");
            buttonAnim.SetTrigger("buttonDown");

            provodMat.SetColor("_EmissionColor", newColor);

            CameraShake.Instance.ShakeCamera(1f, 0.1f, 1);

            audioSource.clip = buttonOn;
            audioSource.Play();

            if (secondBridgeButtonSc.isPressed)
            {
                pressedBridgeSc.BridgeDown();
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if ((collision.gameObject.layer == 22 || collision.gameObject.tag == "Player") && inButton)
        {
            isPressed = false;

            inButton = false;
            //bridgeAnim.SetTrigger("bridgeUp");
            buttonAnim.SetTrigger("buttonUp");

            provodMat.SetColor("_EmissionColor", standartColor);

            CameraShake.Instance.ShakeCamera(1f, 0.1f, 1);

            audioSource.clip = buttonOff;
            audioSource.Play();

            if (secondBridgeButtonSc.isPressed)
            {
                pressedBridgeSc.BridgeUp();
            }
        }
    }
}
