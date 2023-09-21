using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TriggerWall : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private ChangeView changeViewSc;
    [SerializeField] private Transform mainKrot;

    [Header("Cameras")]
    private GameObject camera1;
    [SerializeField] private GameObject camera2;

    [Header("Krots")]
    [SerializeField] private GameObject[] krots;
    [SerializeField] private GameObject krotController;

    [Header("KrotDialogBox")]
    [SerializeField] private GameObject dlgBox;
    [SerializeField] private GameObject signHello;

    [Header("Close Objects")]
    [SerializeField] private GameObject[] offElements;
    GameObject weapon;

    [Header("CheckWeapon")]
    [SerializeField] private GameObject hands;
    [SerializeField] private CinemachineVirtualCamera cameraView;
    [SerializeField] private GameObject rifleAnimated;
    [SerializeField] private Transform cubeLookAt;

    [Header("CloseDialog")]
    [SerializeField] private GameObject krotDialog;

    [Header("Player")]
    PlayerMovementAdvanced playerMovementAdvanced;

    [Header("AudioPitch")]
    [SerializeField] private Animator audioPitch;
    [SerializeField] private AudioSource mainAudioSource;
    [SerializeField] private AudioClip battleAudio;
    [SerializeField] private GameObject ambientAudio;

    AudioSource audioSource;

    private void Awake()
    {
        playerMovementAdvanced = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementAdvanced>();
        camera1 = GameObject.FindGameObjectWithTag("CMcam1");
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!DifferentStatic.wallEnter)
        {
            //audioSource.Play();
            audioSource.pitch = 1f;
            wall.SetActive(true);
            DifferentStatic.wallEnter = true;

            StartCoroutine("StartCutScene");
        }
    }

    IEnumerator StartCutScene()
    {
        audioPitch.SetTrigger("PitchDown");
        mainAudioSource.clip = battleAudio;
        mainAudioSource.Play();
        ambientAudio.SetActive(true);

        yield return new WaitForSeconds(1);

        playerMovementAdvanced.stopMoving = true;

        for (int i = 0; i < offElements.Length; i++)
            offElements[i].SetActive(false);

        if (DifferentStatic.isWeaponPickup)
        {
            weapon = GameObject.FindGameObjectWithTag("WeaponHolder");
            weapon.SetActive(false);
        }

        camera1.SetActive(false);
        camera2.SetActive(true);
        yield return new WaitForSeconds(2);

        for (int i = 0; i < krots.Length; i += 2)
        {
            krots[i].SetActive(true);
            krots[i + 1].SetActive(true);
            yield return new WaitForSeconds(.5f);
            audioSource.Play();
            audioSource.pitch -= .15f;
            CameraShake.Instance.ShakeCamera(5f, 0.15f, 1);
            //yield return new WaitForSeconds(.5f);
        }
        //mainKrot.gameObject.SetActive(true);
        //CameraShake.Instance.ShakeCamera(3f, 0.1f, 1);

        yield return new WaitForSeconds(1);
        changeViewSc.ChangeViewToAnother(mainKrot);
        mainKrot.gameObject.SetActive(true);
        CameraShake.Instance.ShakeCamera(5f, 0.15f, 1);

        yield return new WaitForSeconds(2);
        CameraShake.Instance.ShakeCamera(5f, 0.15f, 1);
        audioSource.pitch = 1f;
        audioSource.Play();
        dlgBox.SetActive(true);
        signHello.SetActive(true);

        yield return new WaitForSeconds(3);
        changeViewSc.ChangeViewToPlayer();

        CheckWeapon();
    }

    void CheckWeapon()
    {
        if (DifferentStatic.isWeaponPickup)
            SetController();
        else
        {
            StartCoroutine("AfterCameraKrot");
        }
    }

    IEnumerator AfterCameraKrot()
    {
        cameraView.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        hands.SetActive(true);
        yield return new WaitForSeconds(5f);
        hands.SetActive(false);
        cameraView.LookAt = cubeLookAt;

        krotDialog.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        rifleAnimated.SetActive(true);
    }

    public void ResetAfterCameraKrot()
    {
        playerMovementAdvanced.stopMoving = false;

        //cameraView.GetComponent<Animator>().enabled = false;
        cameraView.gameObject.SetActive(false);

        rifleAnimated.SetActive(false);

        SetController();
    }

    void SetController()
    {
        if (weapon != null)
        {
            weapon.SetActive(true);
        }

        playerMovementAdvanced.stopMoving = false;

        krotController.SetActive(true);
    }
}
