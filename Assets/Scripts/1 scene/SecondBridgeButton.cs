using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.Feedbacks;

public class SecondBridgeButton : MonoBehaviour
{
    //[SerializeField] Animator bridgeAnim;
    Animator buttonAnim;
    [SerializeField] private Material provodMat;

    [Header("FirstButton")]
    [SerializeField] private ButtonBridge buttonBridgeSc;
    [SerializeField] private PressedBridge pressedBridgeSc;

    [Header("Color")]
    [SerializeField] Color newColor = Color.red;
    [SerializeField] Color standartColor;
    AudioSource audioSource;

    [Header("AudioClips")]
    [SerializeField] AudioClip buttonOn;
    [SerializeField] AudioClip buttonOff;

    [Header("Text")]
    [SerializeField] private Animator liftAnim;
    [SerializeField] TextMeshPro textOff;
    [SerializeField] MMFeedbacks textFeedback;

    public bool isPressed = false;

    bool inButton = false;


    void Start()
    {

        provodMat.SetColor("_EmissionColor", standartColor);
        textOff.text = "OFF";
        buttonAnim = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        //provodMat = provod.material;
        //standartColor = provodMat.color;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.layer == 21 || collision.gameObject.tag == "Player") && !inButton)
        {
            isPressed = true;

            inButton = true;

            //bridgeAnim.SetTrigger("bridgeDown");
            buttonAnim.SetTrigger("buttonDown");
            liftAnim.SetTrigger("LiftDown");

            provodMat.SetColor("_EmissionColor", newColor);

            textOff.text = "ON";
            textFeedback?.PlayFeedbacks();

            CameraShake.Instance.ShakeCamera(1f, 0.1f, 1);

            audioSource.clip = buttonOn;
            audioSource.Play();

            if (buttonBridgeSc.isPressed)
            {
                pressedBridgeSc.BridgeDown();
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if ((collision.gameObject.layer == 21 || collision.gameObject.tag == "Player") && inButton)
        {
            isPressed = false;

            inButton = false;
            //bridgeAnim.SetTrigger("bridgeUp");
            buttonAnim.SetTrigger("buttonUp");
            liftAnim.SetTrigger("LiftUp");

            provodMat.SetColor("_EmissionColor", standartColor);

            textOff.text = "OFF";
            textFeedback?.PlayFeedbacks();

            CameraShake.Instance.ShakeCamera(1f, 0.1f, 1);

            audioSource.clip = buttonOff;
            audioSource.Play();

            if (buttonBridgeSc.isPressed)
            {
                pressedBridgeSc.BridgeUp();
            }
        }
    }
}
