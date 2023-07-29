using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class KrotPatterns : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float rotationSpeed = 1.5f;

    public KrotMain krotMainStats;

    [SerializeField] Animator gunAnimator;

    [Header("Effect")]
    [SerializeField] MMFeedbacks shootFeedback;

    [Header("Pattern1")]
    [SerializeField] Transform gun;
    [SerializeField] Transform shootPoint;
    [SerializeField] ParticleSystem plasmaMuzzleFlash;
    [SerializeField] GameObject plasmaShoot;

    [Header("Audio")]
    [SerializeField] AudioClip plasmaSFX;
    [SerializeField] AudioClip rocketSFX;
    [SerializeField] AudioSource audioSource;

    [Header("Pattern2")]
    [SerializeField] GameObject rocket;
    [SerializeField] GameObject rocketDown;

    [Header("State")]
    [SerializeField] bool isActive;
    public bool IsActive => isActive;

    public bool isRocketAttack;

    [Header("TimeActive")]
    [SerializeField] int minActive = 4;
    [SerializeField] int maxActive = 8;

    bool rotateToPlayer = false;

    Animator animator;


    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        animator.enabled = true;
        gunAnimator.enabled = true;

        isActive = true;

        Invoke("ResetAnimation", .8f);


        if (isRocketAttack)
        {
            Invoke("Pattern2Rocket", .9f);
        }
    }

    void OnDisable()
    {
        isActive = false;
    }

    void ResetAnimation()
    {
        animator.enabled = false;
        gunAnimator.enabled = false;
        rotateToPlayer = true;
    }

    public void Pattern1ForControl()
    {
        Invoke("DisableKrot", Random.Range(minActive, maxActive));

        Invoke("Pattern1Shoot", Random.Range((minActive / 2) - 1, (minActive / 2) + 1));
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
            Pattern1Shoot();

        if (Input.GetKeyUp(KeyCode.O))
            Pattern2Rocket();

        if (rotateToPlayer)
        {
            var direction = (player.position - gun.position).normalized;
            direction.y = 0f;
            gun.rotation = Quaternion.RotateTowards(gun.rotation, Quaternion.LookRotation(direction), rotationSpeed);
        }
    }

    public void DisableKrot()
    {
        animator.enabled = true;
        animator.SetTrigger("Escape");

        gunAnimator.enabled = true;
        gunAnimator.SetTrigger("End");

        Invoke("EscapeKrot", .8f);
    }

    void EscapeKrot()
    {
        gameObject.SetActive(false);
    }

    public void Pattern2Rocket()
    {
        gunAnimator.enabled = true;
        rotateToPlayer = false;

        gunAnimator.SetTrigger("Rocket");

        Invoke("CreateRocket", 1.2f);
    }

    void CreateRocket()
    {
        Instantiate(rocket, shootPoint.position, shootPoint.rotation);

        audioSource.clip = rocketSFX;
        audioSource.Play();

        shootFeedback?.PlayFeedbacks();
        CameraShake.Instance.ShakeCamera(4f, 0.1f, 1);

        Invoke("RocketDown", 2);
    }

    void RocketDown()
    {
        Instantiate(rocketDown, new Vector3(Random.Range(player.position.x - 8, player.position.x + 8),
                                        50,
                                        Random.Range(player.position.z - 8, player.position.z + 8)), Quaternion.Euler(-180, 0, 0));
    }

    void Pattern1Shoot()
    {
        rotateToPlayer = true;

        Invoke("CreatePlasm", .4f);
    }

    void CreatePlasm()
    {
        if (!isActive)
            return;

        ResetRotate();

        audioSource.clip = plasmaSFX;
        audioSource.Play();

        plasmaMuzzleFlash.Play();
        Instantiate(plasmaShoot, shootPoint.position, shootPoint.rotation);
        Invoke("Pattern1Effect", 1);
    }

    void Pattern1Effect()
    {
        plasmaMuzzleFlash.Play();
        shootFeedback?.PlayFeedbacks();
        CameraShake.Instance.ShakeCamera(2.5f, 0.1f, 1);

        rotateToPlayer = true;
    }

    void ResetRotate()
    {
        rotateToPlayer = false;
    }
}
