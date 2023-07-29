using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBridge : MonoBehaviour
{
    [SerializeField] Animator bridgeAnim;
    Animator buttonAnim;
    [SerializeField] Renderer provod;
    Material provodMat;

    [Header("Color")]
    [SerializeField] Color newColor = Color.red;
    Color standartColor;
    AudioSource audioSource;

    [Header("AudioClips")]
    [SerializeField] AudioClip buttonOn;
    [SerializeField] AudioClip buttonOff;

    bool inButton = false;


    void Start()
    {
        buttonAnim = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        provodMat = provod.material;
        standartColor = provodMat.color;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.layer == 18 || collision.gameObject.tag == "Player") && !inButton)
        {
            inButton = true;
            print(collision.gameObject.name);

            bridgeAnim.SetTrigger("bridgeDown");
            buttonAnim.SetTrigger("buttonDown");

            provodMat.SetColor("_EmissionColor", newColor);
            CameraShake.Instance.ShakeCamera(1f, 0.1f, 1);

            audioSource.clip = buttonOn;
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if ((collision.gameObject.layer == 18 || collision.gameObject.tag == "Player") && inButton)
        {
            inButton = false;
            bridgeAnim.SetTrigger("bridgeUp");
            buttonAnim.SetTrigger("buttonUp");

            provodMat.SetColor("_EmissionColor", standartColor);
            CameraShake.Instance.ShakeCamera(1f, 0.1f, 1);

            audioSource.clip = buttonOff;
            audioSource.Play();
        }
    }

    void EnterEffect()
    {

    }

}
