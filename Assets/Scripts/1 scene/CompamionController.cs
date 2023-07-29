using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompamionController : MonoBehaviour
{
    [SerializeField] NavMeshAgent AIAgent;
    [SerializeField] Transform player;

    [SerializeField] Transform[] randomPoints;
    int randomIndex;
    public bool lookAtPlayer = true;

    [Range(0, 20)]
    public float rotationSpeed;

    void Start()
    {

    }


    void Update()
    {
        AIAgent.destination = player.position;

        if (AIAgent.remainingDistance >= AIAgent.stoppingDistance)
            FollowPlayer();
        else
            AboutPlayer();

        print(AIAgent.remainingDistance);

    }

    void FollowPlayer()
    {
        lookAtPlayer = true;

        transform.LookAt(player);
    }

    void AboutPlayer()
    {
        lookAtPlayer = false;

        transform.LookAt(player);

        //StartCoroutine("LookAtRandomPoints");
    }

    IEnumerator LookAtRandomPoints()
    {
        yield return new WaitForSeconds(2);

        while (lookAtPlayer == false)
        {
            yield return new WaitForSeconds(3);
            randomIndex = Random.Range(0, randomPoints.Length);
        }
    }
}