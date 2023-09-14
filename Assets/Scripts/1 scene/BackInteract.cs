using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class BackInteract : MonoBehaviour, IInteractable
{
    [SerializeField] Animator backAnim;
    [SerializeField] Transform player;
    [SerializeField] Transform pointBack;
    [Header("Урон")]
    [SerializeField] int damage;
    [SerializeField] PlayerData playerData;

    [Header("Удар")]
    [SerializeField] float rotationSpeed = 1.5f;
    [SerializeField] MMFeedbacks hitFeedback;
    [SerializeField] private Transform pointHit;
    [SerializeField] private float attackRange = 1;

    [Space]
    [Header("Камера")]
    [SerializeField] ChangeView changeViewSc;

    [Space]
    [Header("Диалог")]
    [SerializeField] GameObject uiCanvas;

    bool rotateToPlayer = false;
    bool oneTime = false;

    bool isInteract = false;
    public GameObject[] highlightObj;

    bool isHit = false;



    public void Highlight()
    {
        for (int i = 0; i < highlightObj.Length; i++)
            highlightObj[i].layer = LayerMask.NameToLayer("Interactable");
    }

    public void Interact()
    {
        print("interact");

        DifferentStatic.isBackInteract = true;

        if (oneTime)
            return;

        oneTime = true;

        rotateToPlayer = true;
        Invoke("ResetRotation", .6f);
    }

    public void AfterInteract()
    {
        backAnim.SetTrigger("Idle");
    }

    void HitAnimation()
    {
        isHit = true;

        backAnim.SetTrigger("Attack");
        Invoke("MeleeStrike", .85f);
    }

    void MeleeStrike()
    {
        Collider[] hitCollider = Physics.OverlapSphere(pointHit.position, attackRange);

        foreach (Collider collider in hitCollider)
        {
            print(collider.name);
            if (collider.tag == "Player")
            {
                //collider.GetComponent<PersonMain>().TakeDamage(damage);
                //CameraShake.Instance.ShakeCamera(2.1f, 0.1f, 1);

                Hit();
            }
        }
    }


    void Hit()
    {
        playerData.health -= damage;

        hitFeedback?.PlayFeedbacks();
        CameraShake.Instance.ShakeCamera(4f, 0.2f, 1);

        ChangeCameraLook();
    }

    private void Update()
    {
        if (rotateToPlayer)
        {
            var direction = (player.position - pointBack.position).normalized;
            direction.y = 0f;
            pointBack.rotation = Quaternion.RotateTowards(pointBack.rotation, Quaternion.LookRotation(direction), rotationSpeed);

            if (pointBack.rotation == Quaternion.LookRotation(direction))
            {
                ResetRotation();
                print("Поворот");
            }
        }
    }

    void ResetRotation()
    {
        if (!isHit)
            HitAnimation();

        rotateToPlayer = false;
    }

    void ChangeCameraLook()
    {
        changeViewSc.ChangeViewToAnother(pointBack);

        Invoke("DialogActive", 2);
    }

    void DialogActive()
    {
        uiCanvas.SetActive(true);
    }
}
