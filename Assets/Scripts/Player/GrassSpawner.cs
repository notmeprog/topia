using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class GrassSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject particleGrass;
    public GameObject particleGrassV2;
    public GameObject circleParticle;
    public GameObject flowerParticle;

    [Header("Settings")]
    [SerializeField] private LayerMask spawnMask;
    [SerializeField] Camera playerCamera;
    [SerializeField] private float spawnRange;

    [Header("Effect")]
    [SerializeField] MMFeedbacks spawnFeedback;

    void Update()
    {
        if (Input.GetButtonUp("Fire2"))
        {
            print("Fire2");
            RaycastHit hit;
            //Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit))
            {
                Instantiate(particleGrassV2, hit.point + new Vector3(0, .1f, 0), Quaternion.LookRotation(hit.normal));
                Instantiate(particleGrass, hit.point + new Vector3(0, .1f, 0), Quaternion.LookRotation(hit.normal));
                Instantiate(flowerParticle, hit.point + new Vector3(0, .1f, 0), Quaternion.LookRotation(hit.normal));
                Instantiate(circleParticle, hit.point + new Vector3(0, .5f, 0), Quaternion.identity);

                Invoke("Effect", .4f);
                ///particleSystem.transform.LookAt(transform.position + hit.normal);
            }
        }
    }

    void Effect()
    {
        spawnFeedback?.PlayFeedbacks();

        CameraShake.Instance.ShakeCamera(4f, 0.2f, 1);
    }
}
