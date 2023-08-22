using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LaxDestination : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform laxDestination;

    public SpriteRenderer spriteRenderer;
    public SpriteRenderer spriteInventory;
    public Sprite backSprite;

    public Animator animator;

    bool check = false;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //Invoke("GoAway", 2);
    }

    // Update is called once per frame
    public void GoAway()
    {
        agent.SetDestination(laxDestination.position);

        Invoke("ChangeSprite", 8);
    }

    void ChangeSprite()
    {
        animator.SetTrigger("StopMoving");

        spriteRenderer.sprite = backSprite;
        spriteInventory.sortingOrder = 1;
    }
}
