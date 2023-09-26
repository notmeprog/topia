using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class CheckWin : MonoBehaviour
{
    [SerializeField] private KrotMain[] krotMains;
    private KrotMain mainKrotSc;

    [Header("EndBattle")]
    AudioSource audioSource;
    [SerializeField] private Animator mainAudio;
    [SerializeField] private GameObject[] krots;
    [SerializeField] private Animator[] krotAnims;
    [SerializeField] private KrotController krotControllerSc;
    [SerializeField] private GameObject mainKrot;
    [SerializeField] private Animator wall;

    [Header("Effects")]
    [SerializeField] MMFeedbacks endFeedback;
    [SerializeField] MMFeedbacks secondEndFeedback;

    [Header("MusicAfterFight")]
    [SerializeField] private AudioClip audioNormal;

    [Header("Вторая концовка")]
    [SerializeField] private Animator ambientAudio;
    [SerializeField] private Animator mainAmbient;

    bool isFoundEnd = false;

    private void Awake()
    {
        audioSource = mainAudio.GetComponent<AudioSource>();

        mainKrotSc = mainKrot.GetComponent<KrotMain>();
    }

    private void Update()
    {
        if (CheckCountOfDeath() >= 3 && !isFoundEnd)
        {
            StartCoroutine("ThirdKrotDead");
            isFoundEnd = true;
        }
        else if (mainKrotSc.isDead && CheckCountOfDeath() == 2 && !isFoundEnd)
        {
            StartCoroutine("MainAndTwoDead");
            isFoundEnd = true;
        }
        else if (CheckCountOfMercy() >= 3 && !isFoundEnd)
        {
            StartCoroutine("ThirdKrotMercy");
            isFoundEnd = true;
        }
        else if ((CheckCountOfMercy() + CheckCountOfDeath()) >= 3 && !isFoundEnd)
        {
            StartCoroutine("ThirdKrotDead");
            isFoundEnd = true;
        }

        if (Input.GetKeyUp(KeyCode.E))
            StartCoroutine("MainAndTwoDead");
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

    int CheckCountOfMercy()
    {
        int countOfMercy = 0;

        for (int i = 0; i < krotMains.Length; i++)
        {
            if (krotMains[i].isMercyEscape)
                countOfMercy++;
        }

        return countOfMercy;
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

    IEnumerator ThirdKrotMercy()
    {
        StartCoroutine("EndBattle");
        yield return new WaitForSeconds(2f);
        audioSource.clip = audioNormal;
        endFeedback?.PlayFeedbacks();

        yield return new WaitForSeconds(2f);
        audioSource.pitch = 1f;
        audioSource.Play();
    }

    IEnumerator MainAndTwoDead()
    {
        StartCoroutine("EndBattle");

        mainAudio.SetTrigger("MusicDown");
        mainAmbient.SetTrigger("UpVolume");

        secondEndFeedback?.PlayFeedbacks();
        yield return new WaitForSeconds(2f);
        //audioSource.clip = audioNormal;    //это чтобы выклчить музыку. я решил не использвать Stop
        audioSource.Stop();


        ambientAudio.SetTrigger("EndBattle");
    }

    IEnumerator EndBattle()
    {
        DifferentStatic.endBattle = true;

        krotControllerSc.enabled = false;
        mainKrot.SetActive(false);

        for (int i = 0; i < krots.Length; i++)
        {
            if (!krotMains[i].isDead)
            {
                krotAnims[i].enabled = true;
                krotAnims[i].SetTrigger("Escape");
            }
        }
        yield return new WaitForSeconds(.7f);

        for (int i = 0; i < krots.Length; i++)
        {
            if (!krotMains[i].isDead)
                krots[i].SetActive(false);
        }

        wall.SetTrigger("Out");

        yield return new WaitForSeconds(2f);
        wall.gameObject.SetActive(false);
    }
}
