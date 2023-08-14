using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneCroco : MonoBehaviour
{
    [SerializeField] GameObject crocoObj;
    [Header("Камера")]
    [SerializeField] ChangeView changeViewSc;
    [SerializeField] PlayerMovementAdvanced playerMovementAdvancedSc;

    [SerializeField] private Animator radioAudioPitch;
    [SerializeField] EnemyMain crocoHealthSc;

    AudioSource audioSource;

    bool oneTime = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetCutscene()
    {
        changeViewSc.ChangeViewToAnother(crocoObj.transform);

        playerMovementAdvancedSc.stopMoving = true;

        radioAudioPitch.SetTrigger("PitchDown");

        Invoke("ActiveCroco", 2);

        Invoke("CloseCutscene", 4);
    }

    void ActiveCroco()
    {
        audioSource.Play();
        crocoObj.SetActive(true);
    }

    void CloseCutscene()
    {
        playerMovementAdvancedSc.stopMoving = false;
        changeViewSc.ChangeViewToPlayer();
    }

    void Update()
    {
        if (crocoHealthSc.isDead && !oneTime)
        {
            radioAudioPitch.SetTrigger("PitchUp");
            oneTime = true;
        }
    }
}