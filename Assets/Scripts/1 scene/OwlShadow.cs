using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlShadow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject featherParticles;
    [SerializeField] private AudioSource audioSource;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < 12 && !DifferentStatic.isOwlShadowInteract)
        {
            DifferentStatic.isOwlShadowInteract = true;

            if (featherParticles != null)
                EffectDisable();

            gameObject.SetActive(false);

        }

    }

    void EffectDisable()
    {
        featherParticles.SetActive(true);
        CameraShake.Instance.ShakeCamera(5f, 0.2f, 1);
        audioSource.Play();
    }
}
