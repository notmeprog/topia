using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlShadow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject featherParticles;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < 10)
        {
            EffectDisable();

            gameObject.SetActive(false);
        }

    }

    void EffectDisable()
    {
        featherParticles.SetActive(true);
    }
}
