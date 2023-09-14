using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnd : MonoBehaviour
{
    Shotgun shotgunSc;
    [SerializeField] private GameObject panelEnd;
    [SerializeField] private Animator pitchAudio;

    GameObject player;
    PlayerMovementAdvanced playerMovementAdvanced;
    Rigidbody playerRigidbody;
    AudioSource audioSource;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerMovementAdvanced = player.GetComponent<PlayerMovementAdvanced>();
        playerRigidbody = player.GetComponent<Rigidbody>();

        shotgunSc = FindObjectOfType<Shotgun>();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerMovementAdvanced.stopMoving = true;

            playerMovementAdvanced.isLoadingLevel = true;
            playerRigidbody.useGravity = false;

            if (shotgunSc != null)
                shotgunSc.canShoot = false;

            panelEnd.SetActive(true);
            audioSource.Play();
            pitchAudio.SetTrigger("MusicDown");

            print("perehod");
        }
    }
}
