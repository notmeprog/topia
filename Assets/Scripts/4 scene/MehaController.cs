using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmazingAssets.AdvancedDissolve;

public class MehaController : MonoBehaviour
{
    [SerializeField] GameObject mehaObject;
    [SerializeField] Material krotMaterial;
    [SerializeField] ParticleSystem particleDissolve;

    bool isDissolvingEnd = false;
    bool isDissolvingStart = false;
    float speedDissolve = .75f;
    float fade = 0;
    void Start()
    {

    }

    void EnableMeha()
    {
        mehaObject.SetActive(true);

        fade = 1;
        isDissolvingStart = true;
        particleDissolve.Play();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Y))
        {
            fade = 1;
            isDissolvingStart = true;
            particleDissolve.Play();
        }

        if (isDissolvingStart)
        {
            fade -= Time.deltaTime * speedDissolve;

            if (fade <= 0f)
            {
                fade = 0;
                isDissolvingStart = false;
            }

            AdvancedDissolveProperties.Cutout.Standard.UpdateLocalProperty
            (
            krotMaterial,
            AdvancedDissolveProperties.Cutout.Standard.Property.Clip,
            fade
            );
        }

        if (isDissolvingEnd)
        {
            fade += Time.deltaTime * speedDissolve;

            if (fade >= 1f)
            {
                fade = 1;
                isDissolvingEnd = false;
            }

            AdvancedDissolveProperties.Cutout.Standard.UpdateLocalProperty
            (
            krotMaterial,
            AdvancedDissolveProperties.Cutout.Standard.Property.Clip,
            fade
            );
        }
    }
}
