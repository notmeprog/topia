using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWeaponOutline : MonoBehaviour
{
    public GameObject weaponOutlineImg;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void EnableOutline()
    {
        weaponOutlineImg.SetActive(true);
    }

    public void AudioFlash()
    {
        audioSource.pitch = Random.Range(.9f, 1);
        audioSource.Play();
    }
}
