using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleSprite : MonoBehaviour
{
    public Transform player;
    private Vector3 targetPos;
    private Vector3 targetDir;

    public float angle;
    public bool faceToPlayer = false;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        targetDir = targetPos - transform.position;

        angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

        faceToPlayer = GetIndex(angle);

        ///animator.SetBool("Walk", faceToPlayer);
    }

    bool GetIndex(float angle)
    {
        if (angle > -90 && angle < 90)
        {
            print("front");
            return true;
        }
        else
        {
            print("back");
            return false;
        }
    }
}
