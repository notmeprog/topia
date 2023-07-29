using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDown : MonoBehaviour
{
    public float speed;
    [SerializeField] private float destroyTime;
    [SerializeField] GameObject hitEffect;
    [SerializeField] int damage = 30;

    [Header("ХП игрока")]
    [SerializeField] private PlayerData playerData;

    AudioSource audioSource;

    Vector3 lastPosition;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position;
        //Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        RaycastHit hitInfo;

        if (Physics.Linecast(lastPosition, transform.position, out hitInfo))
        {
            if (hitInfo.collider.tag != "Trigger")
            {
                if (hitInfo.collider.tag == "Player")
                    playerData.health -= damage;

                Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                audioSource.Play();
                CameraShake.Instance.ShakeCamera(5f, 0.3f, 1);
                Destroy(gameObject, 1);
            }
        }

        lastPosition = transform.position;
    }
}
