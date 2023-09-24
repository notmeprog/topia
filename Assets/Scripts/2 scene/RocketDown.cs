using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDown : MonoBehaviour
{
    public float speed;
    [SerializeField] private float destroyTime;
    [SerializeField] GameObject hitEffect;
    [SerializeField] int damage = 30;
    [SerializeField] private float attackRange = 1;

    [Header("ХП игрока")]
    [SerializeField] private PlayerData playerData;

    AudioSource audioSource;

    bool oneTime = false;

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
            if (hitInfo.collider.tag != "Trigger" && !oneTime)
            {
                Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                audioSource.Play();
                CameraShake.Instance.ShakeCamera(7f, .5f, 2);
                Destroy(gameObject, 1);

                oneTime = true;
            }
        }

        lastPosition = transform.position;



        Collider[] hitCollider = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider collider in hitCollider)
        {
            if (collider.tag == "Player" && !oneTime)
            {
                playerData.health -= damage;

                Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                audioSource.Play();
                CameraShake.Instance.ShakeCamera(7f, .5f, 2);
                Destroy(gameObject, 1);

                oneTime = true;
            }
        }
    }
}
