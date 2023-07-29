using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class VineGrowth : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] Camera playerCamera;

    [Header("Prefabs")]
    [SerializeField] GameObject vine;
    [SerializeField] GameObject vineSmall;
    public GameObject circleParticle;

    [Header("Effect")]
    [SerializeField] MMFeedbacks vineFeedback;

    bool canSpawn = true;
    [SerializeField] GameObject cooldownImage;

    [Header("Destroy")]
    [SerializeField] GameObject destroyParticle;
    [SerializeField] AudioSource audioSource;

    GameObject[] vines = new GameObject[8];
    GameObject[] smallVines = new GameObject[8];


    void Update()
    {
        if (Input.GetButtonUp("Fire2"))
        {
            CreateWall();
        }
    }

    void CreateWall()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit))
        {
            if (hit.collider.tag == "VineWall" && canSpawn)
            {
                VineLayer currentLayer = hit.collider.GetComponent<VineLayer>();

                if (DifferentStatic.isSpawnedVine)
                {
                    //DifferentStatic.destroyVine = true;
                    DifferentStatic.isSpawnedVine = false;

                    DestroyVines(currentLayer.countVines);
                }
                else
                {
                    DifferentStatic.isSpawnedVine = true;

                    canSpawn = false;
                    cooldownImage.SetActive(true);

                    Invoke("ResetCDImage", 3);

                    StartCoroutine(CreateVines(currentLayer.countVines, hit.point));

                    Instantiate(circleParticle, hit.point + new Vector3(0, .5f, 0), Quaternion.identity);
                    Invoke("Effect", 0.4f);
                }
            }
        }
    }

    void DestroyVines(int n)
    {
        audioSource.Play();
        CameraShake.Instance.ShakeCamera(2f, .1f, 1);

        for (int i = 0; i < n; i++)
        {
            Instantiate(destroyParticle, vines[i].transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            Destroy(vines[i]);

            if (smallVines[i] != null)
            {
                Instantiate(destroyParticle, smallVines[i].transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                Destroy(smallVines[i]);
            }
        }
    }

    void ShakeGrouth()
    {
        CameraShake.Instance.ShakeCamera(1f, 1f, 1);
    }

    void ResetCDImage()
    {
        canSpawn = true;
        cooldownImage.SetActive(false);
    }

    void Effect()
    {
        CameraShake.Instance.ShakeCamera(4f, 0.2f, 1);
        vineFeedback?.PlayFeedbacks();
    }

    IEnumerator CreateVines(int n, Vector3 hitPos)
    {
        yield return new WaitForSeconds(.4f);

        ShakeGrouth();

        float randomOffset = 0;
        float flipVine = 1;
        for (int i = 0; i < n; i++)
        {
            print(i);
            randomOffset = i * Random.Range(0.1f, 0.2f) * flipVine;

            vines[i] = Instantiate(vine, new Vector3(hitPos.x + randomOffset, hitPos.y, hitPos.z + Random.Range(-.1f, .1f)), Quaternion.Euler(
                                                                                                Random.Range(-5, 5),
                                                                                                Random.Range(0, 360),
                                                                                                0
            ));

            flipVine *= -1;

            yield return new WaitForSeconds(0.1f);

            if (i <= (n / 2) + 1)
            {
                flipVine *= -1;
                randomOffset = i * Random.Range(0.1f, 0.3f) * flipVine;
                smallVines[i] = Instantiate(vineSmall, new Vector3(hitPos.x + randomOffset, hitPos.y, hitPos.z + Random.Range(-.1f, .1f)), Quaternion.Euler(
                                                                                                Random.Range(-5, 5),
                                                                                                Random.Range(0, 360),
                                                                                                0
                ));
                flipVine *= -1;
            }
        }
    }
}
