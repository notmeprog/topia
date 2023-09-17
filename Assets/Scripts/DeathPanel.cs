using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathPanel : MonoBehaviour
{
    [SerializeField] private Animator audioPitch;
    [SerializeField] private Animator knob;
    [SerializeField] private Animator panelDeath;

    [Header("Transition")]
    [SerializeField] private GameObject transitionIn;
    [SerializeField] private GameObject transitionOut;

    [Header("Health Data")]
    [SerializeField] private PlayerData playerData;

    [Header("Teleport")]
    [SerializeField] private Transform pointRespawn;
    private Transform player;
    [SerializeField] private GameObject mainPanel;

    [Header("PressEffect")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI textRestart;

    bool oneTime = false;
    bool canEnter = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        Invoke("Death", 1);

        oneTime = false;
        canEnter = false;

        StartCoroutine("SetEnter");

        textRestart.text = "<wave a=.8 f=.25>press Enter to restart</wave>";
    }

    IEnumerator SetEnter()
    {
        yield return new WaitForSecondsRealtime(2);
        canEnter = true;
    }

    void Death()
    {
        Time.timeScale = 0f;
        audioPitch.SetTrigger("PitchDownDeath");
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return) && !oneTime && canEnter)
        {
            oneTime = true;
            StartCoroutine("Restart");
            Effect();
        }
    }

    void Effect()
    {
        audioSource.Play();
        textRestart.text = "<color=yellow><wave a=.8 f=1>press Enter to restart</wave></color>";
        CameraShake.Instance.ShakeCamera(3f, 0.1f, 1);
    }

    IEnumerator Restart()
    {
        knob.SetTrigger("Restart");
        yield return new WaitForSecondsRealtime(1);

        player.position = pointRespawn.position;
        playerData.health = 10;
        Time.timeScale = 1f;

        transitionIn.SetActive(false);
        transitionOut.SetActive(true);
        audioPitch.SetTrigger("PitchUpDeath");
        panelDeath.SetTrigger("PanelOut");

        yield return new WaitForSecondsRealtime(2);
        transitionIn.SetActive(true);
        transitionOut.SetActive(false);
        mainPanel.SetActive(false);
    }
}
