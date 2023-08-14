using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class Shotgun : MonoBehaviour
{
    public int damage = 60;
    [SerializeField] float range = 6;
    [SerializeField] float forceHit = 160f;
    [SerializeField] LayerMask layerMaskShoot;

    [Header("Effect")]
    [SerializeField] MMFeedbacks fireFeedback;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] AudioClip shotSFX;
    AudioSource audioSource;
    Animator animShotgun;

    [Header("Hit")]
    [SerializeField] private GameObject spriteMaskEn;
    [SerializeField] private GameObject bloodParticle;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Animator crosshairAnim;

    [Header("Reload")]
    [SerializeField] WeaponReload weaponReloadSc;

    float nextFire = 0;
    public float fireRate = 0.1f;
    Camera mainCamera;

    TimeFreezer timeFreezerSc;

    PlayerMovementAdvanced playerMovementAdvanced;


    void Awake()
    {
        playerMovementAdvanced = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementAdvanced>();
    }

    void Start()
    {
        mainCamera = Camera.main;
        audioSource = GetComponent<AudioSource>();

        timeFreezerSc = GetComponent<TimeFreezer>();

        animShotgun = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire && weaponReloadSc.ammoCount > 0)
        {
            nextFire = Time.time + 1f / fireRate;
            Shoot();

            weaponReloadSc.ammoCount--;
            weaponReloadSc.UpdateText();
        }

        if (playerMovementAdvanced.state == PlayerMovementAdvanced.MovementState.sprinting)
            animShotgun.SetBool("Run", true);
        else
            animShotgun.SetBool("Run", false);
    }

    void Shoot()
    {
        audioSource.pitch = Random.Range(.8f, 1);
        audioSource.PlayOneShot(shotSFX);

        muzzleFlash.Play();

        CameraShake.Instance.ShakeCamera(5f, 0.2f, 1);
        fireFeedback?.PlayFeedbacks();

        animShotgun.SetTrigger("Recoil");

        RaycastHit hit;

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range, layerMaskShoot))
        {
            GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2);

            print(hit.collider.name);


            if (hit.rigidbody != null)
            {
                crosshairAnim.SetTrigger("Hit");

                hit.rigidbody.AddForce(-hit.normal * forceHit);
            }

            if (hit.collider.tag == "Krot")
            {
                timeFreezerSc.FreezeTime(.1f);
                crosshairAnim.SetTrigger("Hit");

                KrotMain currentHit = hit.collider.GetComponent<KrotMain>();
                if (currentHit != null)
                    currentHit.TakeDamage(damage);
            }
            else if (hit.collider.tag == "Destroyable")
            {
                timeFreezerSc.FreezeTime(.1f);
                crosshairAnim.SetTrigger("Hit");

                DestroyableMain currentHit = hit.collider.GetComponent<DestroyableMain>();
                if (currentHit != null)
                    currentHit.TakeDamage();
            }
            else if (hit.collider.tag == "SimpleEnemy")
            {

                UseSpriteMask(hit, hit.collider.transform);

                timeFreezerSc.FreezeTime(.1f);
                crosshairAnim.SetTrigger("Hit");

                EnemyMain enemy = hit.collider.GetComponent<EnemyMain>();
                if (enemy != null)
                    enemy.TakeDamage(damage);
            }
        }
    }

    void UseSpriteMask(RaycastHit hit, Transform parent)
    {
        GameObject spriteMask1 = Instantiate(spriteMaskEn, hit.point, Quaternion.LookRotation(hit.normal));

        spriteMask1.transform.SetParent(parent.Find("Sprite"));

        Instantiate(bloodParticle, hit.point, Quaternion.LookRotation(-hit.normal));
    }
}
