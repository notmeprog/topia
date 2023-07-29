using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdInteract : MonoBehaviour, IInteractable
{
    Animator animator;

    AudioSource audioSource;

    float nextFire = 0;
    public float fireRate = 0.1f;

    public AudioClip[] birdSounds;

    int countInteract = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }


    public void Highlight()
    {
        //
    }

    public void Interact()
    {
        if (Time.time > nextFire)
        {
            if (countInteract < 5)
            {
                animator.SetTrigger("Interact");
                audioSource.PlayOneShot(birdSounds[Random.Range(0, birdSounds.Length)]);

                nextFire = Time.time + 1f / fireRate;

                countInteract++;
            }
            else
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
                animator.SetTrigger("Jump");
                audioSource.PlayOneShot(birdSounds[Random.Range(0, birdSounds.Length)]);
            }
        }
    }

    public void AfterInteract()
    {
        //
    }
}
