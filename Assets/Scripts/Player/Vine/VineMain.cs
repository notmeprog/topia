using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineMain : MonoBehaviour
{
    [SerializeField] GameObject destroyParticle;

    void Start()
    {
        Invoke("DestroyVine", DifferentStatic.lifetimeVine);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
            DifferentStatic.destroyVine = true;

        if (DifferentStatic.destroyVine)
        {
            DestroyVine();
        }
    }

    void DestroyVine()
    {
        DifferentStatic.isSpawnedVine = false;

        Instantiate(destroyParticle, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        Destroy(gameObject);

        DifferentStatic.destroyVine = false;
    }
}
