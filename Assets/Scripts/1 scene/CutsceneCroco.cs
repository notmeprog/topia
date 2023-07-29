using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneCroco : MonoBehaviour
{
    [SerializeField] GameObject crocoObj;
    [Header("Камера")]
    [SerializeField] ChangeView changeViewSc;
    [SerializeField] PlayerMovementAdvanced playerMovementAdvancedSc;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetCutscene()
    {
        changeViewSc.ChangeViewToAnother(crocoObj.transform);

        playerMovementAdvancedSc.stopMoving = true;

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
}