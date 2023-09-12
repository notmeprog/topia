using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class PlaneDeath : MonoBehaviour
{
    [SerializeField] MMFeedbacks planeFeedback;
    [SerializeField] private Transform playerObj;
    [SerializeField] private Transform[] restartPoints;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerData.health -= 10;

            audioSource.Play();
            CameraShake.Instance.ShakeCamera(5f, 2f, 1);
            planeFeedback?.PlayFeedbacks();

            Invoke("Death", 1f);
        }
    }

    void Death()
    {
        Transform closestPoint = restartPoints[0];
        float closestDistance = Vector3.Distance(playerObj.position, restartPoints[0].position);

        for (int i = 1; i < restartPoints.Length; i++)
        {
            float distance = Vector3.Distance(playerObj.position, restartPoints[i].position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPoint = restartPoints[i];
            }
        }

        playerObj.position = closestPoint.position;
    }
}
