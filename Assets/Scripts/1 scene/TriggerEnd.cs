using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnd : MonoBehaviour
{
    [SerializeField] private GameObject panelEnd;
    [SerializeField] private Animator pitchAudio;
    [SerializeField] private PlayerMovementAdvanced playerMovementAdvanced;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerMovementAdvanced.stopMoving = true;
            panelEnd.SetActive(true);
            audioSource.Play();
            pitchAudio.SetTrigger("MusicDown");
        }
    }
}
