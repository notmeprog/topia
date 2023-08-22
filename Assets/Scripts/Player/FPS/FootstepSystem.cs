using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSystem : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip grass;
    [SerializeField] AudioClip concrete;
    [SerializeField] AudioClip forestRoad;
    [SerializeField] AudioClip wood;

    RaycastHit hit;
    [SerializeField] Transform rayStart;
    [SerializeField] float range;
    [SerializeField] LayerMask layerMask;


    public void Footstep()
    {
        if (Physics.Raycast(rayStart.position, rayStart.transform.up * -1, out hit, range, layerMask))
        {
            print("step");
            if (hit.collider.CompareTag("Grass"))
            {
                PlayFootstepSound(grass);
            }
            else if (hit.collider.CompareTag("Concrete"))
            {
                PlayFootstepSound(concrete);
            }
            else if (hit.collider.CompareTag("ForestRoad"))
            {
                PlayFootstepSound(forestRoad);
            }
            else if (hit.collider.CompareTag("Wood"))
            {
                PlayFootstepSound(wood);
            }
        }
    }

    void PlayFootstepSound(AudioClip audio)
    {
        //print("sfxReal");
        audioSource.pitch = Random.Range(.8f, 1);
        audioSource.PlayOneShot(audio);

    }
}
