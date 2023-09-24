using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class CheckWin : MonoBehaviour
{
    [SerializeField] private KrotMain[] krotMains;

    [Header("EndBattle")]
    AudioSource audioSource;
    [SerializeField] private Animator mainAudio;
    [SerializeField] private GameObject[] krots;
    [SerializeField] private Animator[] krotAnims;
    [SerializeField] private KrotController krotControllerSc;
    [SerializeField] private GameObject mainKrot;

    [Header("Effect")]
    [SerializeField] MMFeedbacks endFeedback;

    [Header("MusicAfterFight")]
    [SerializeField] private AudioClip audioNormal;

    bool isFoundEnd = false;

    private void Awake()
    {
        audioSource = mainAudio.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (CheckCountOfDeath() >= 1 && !isFoundEnd)
        {
            StartCoroutine("ThirdKrotDead");
            isFoundEnd = true;
        }

        if (Input.GetKeyUp(KeyCode.E))
            StartCoroutine("EndBattle");
    }

    int CheckCountOfDeath()
    {
        int countOfDead = 0;

        for (int i = 0; i < krotMains.Length; i++)
        {
            if (krotMains[i].isDead)
                countOfDead++;
        }

        return countOfDead;
    }

    IEnumerator ThirdKrotDead()
    {
        StartCoroutine("EndBattle");

        mainAudio.SetTrigger("MusicDown");
        yield return new WaitForSeconds(3f);

        audioSource.clip = audioNormal;
        endFeedback?.PlayFeedbacks();

        yield return new WaitForSeconds(2f);
        audioSource.pitch = .8f;
        audioSource.Play();
    }

    void FourthKrotMercy()
    {

    }

    void MainAndTwoDead()
    {
    }

    IEnumerator EndBattle()
    {
        DifferentStatic.endBattle = true;

        krotControllerSc.enabled = false;
        mainKrot.SetActive(false);

        for (int i = 0; i < krots.Length; i++)
        {
            krotAnims[i].enabled = true;
            krotAnims[i].SetTrigger("Escape");
        }
        yield return new WaitForSeconds(.7f);

        for (int i = 0; i < krots.Length; i++)
        {
            krots[i].SetActive(false);
        }
    }
}
