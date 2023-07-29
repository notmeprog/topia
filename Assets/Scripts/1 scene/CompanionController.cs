using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private float followDistance = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private Transform[] objectsToLookAt;

    private bool isFollowing = false;
    private bool isLooking = false;
    private Transform currentObjectToLookAt;

    private void Start()
    {
        currentObjectToLookAt = GetRandomObjectToLookAt();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > followDistance)
        {
            // Следование за игроком
            isFollowing = true;
            isLooking = false;
            agent.SetDestination(player.position);
        }
        else
        {
            // Летит рядом и смотрит на объект
            isFollowing = false;
            isLooking = true;
            RotateTowards(currentObjectToLookAt);
        }

        if (isLooking)
        {
            // Если игрок приближается, выбираем новый объект для просмотра
            if (distanceToPlayer < followDistance / 2f)
            {
                currentObjectToLookAt = GetRandomObjectToLookAt();
            }
        }
    }

    private void RotateTowards(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private Transform GetRandomObjectToLookAt()
    {
        if (objectsToLookAt.Length > 0)
        {
            int randomIndex = Random.Range(0, objectsToLookAt.Length);
            return objectsToLookAt[0];
        }

        return null;
    }
}
