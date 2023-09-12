using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private Transform player;
    [SerializeField] private GameObject mainPanel;

    bool oneTime = false;
    bool canEnter = false;

    private void OnEnable()
    {
        Invoke("Death", 1);

        oneTime = false;
        canEnter = false;

        StartCoroutine("SetEnter");
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
        }
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
