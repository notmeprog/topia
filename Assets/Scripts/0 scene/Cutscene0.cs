using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class Cutscene0 : MonoBehaviour
{

    [Header("Player")]
    [SerializeField] PlayerMovementAdvanced playerMovementAdvanced;

    [Header("Очередь")]
    [SerializeField] private LaxDestination laxDestination;
    [SerializeField] private Animator laxAnim;
    [SerializeField] private Animator dedWalk;
    [SerializeField] private Animator dedSprite;

    [Header("Разговоры с дедом")]
    [SerializeField] private GameObject canvasDedDialog;
    [SerializeField] private DialogTypewrite dialogTypewriteSc;
    bool oneTime = false;

    [Header("Смена камер при ударе")]
    [SerializeField] private GameObject mainCam;
    [SerializeField] private GameObject hitCam;
    [SerializeField] private MMFeedbacks hitFeedback;
    [SerializeField] Animator blinkEyesAnim;

    [Header("После удара")]
    [SerializeField] private GameObject textMovement;


    void Start()
    {
        Cursor.visible = false;

        playerMovementAdvanced.stopMoving = true;

        StartCoroutine("QueueTwoPersons");
    }

    IEnumerator QueueTwoPersons()
    {
        yield return new WaitForSeconds(4);
        laxAnim.enabled = true;
        laxDestination.GoAway();

        yield return new WaitForSeconds(1);
        dedWalk.SetTrigger("Move");
        dedSprite.SetTrigger("Walk");

        yield return new WaitForSeconds(1);
        dedSprite.SetTrigger("Idle");
        OpenDialogWithDed();

        yield return new WaitForSeconds(3);
        dedSprite.SetTrigger("Mad");
    }

    void OpenDialogWithDed()
    {
        canvasDedDialog.SetActive(true);
    }

    IEnumerator HitQueue()
    {
        //mainCam.SetActive(false);
        hitCam.SetActive(true);
        yield return new WaitForSeconds(2);
        hitCam.GetComponent<Animator>().SetTrigger("Hit");
        hitFeedback?.PlayFeedbacks();
        CameraShake.Instance.ShakeCamera(4f, 0.3f, 1);
        blinkEyesAnim.SetTrigger("BlinkFast");

        yield return new WaitForSeconds(1.5f);
        hitCam.SetActive(false);

        yield return new WaitForSeconds(1f);
        textMovement.SetActive(true);
        playerMovementAdvanced.stopMoving = false;
    }

    void Update()
    {
        if (dialogTypewriteSc.endDialog && !oneTime)
        {
            oneTime = true;

            dedWalk.SetTrigger("MoveAway");
            dedSprite.SetTrigger("Walk");

            StartCoroutine("HitQueue");
        }
    }
}
