using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrocoAI : MonoBehaviour
{
    public float attackDistance = 2f; // Дистанция, на которой противник атакует игрока
    public float teleportDistance = 10f; // Дистанция, на которой противник начинает преследовать игрока
    public float attackDelay = 2f; // Задержка перед атакой в секундах
    public int attackDamage = 11; // Урон, наносимый противником
    Transform player; // Ссылка на компонент Transform игрока

    private NavMeshAgent agent;
    public Animator animator;
    private bool isAttacking = false;
    private float attackTimer;

    [Header("ХП игрока")]
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameObject hitEffect;

    [Header("Телепорт")]
    [SerializeField] GameObject crocoShadow;
    bool canTeleport = true;
    [SerializeField] AudioSource audioSource;
    Vector3 direction;


    bool isActive = false;

    void OnEnable()
    {
        animator.SetTrigger("EnableSp");
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Задержка перед началом преследования игрока
        Invoke("StartChasingPlayer", 5f);
    }

    private void Update()
    {
        if (player != null && isActive)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Если противник находится в пределах дистанции преследования игрока
            if (distanceToPlayer <= teleportDistance)
            {
                // Направляем противника к игроку
                agent.SetDestination(player.position);

                // Если противник достиг игрока
                if (distanceToPlayer <= attackDistance)
                {
                    // Начинаем атаку
                    AttackPlayer();
                }
            }
            else if (distanceToPlayer > teleportDistance && canTeleport)
            {
                Teleport();
            }
        }

        // Обновление таймера атаки
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                isAttacking = false;
            }
        }

        // Установка параметров анимации
        animator.SetBool("IsMoving", agent.velocity.magnitude > 0.1f);
        animator.SetBool("IsAttacking", isAttacking);

        direction = player.position - transform.position;
        // print(player.position + "  --  " + transform.position);

    }

    void Teleport()
    {
        Instantiate(crocoShadow, transform.position, Quaternion.identity);

        audioSource.Play();

        transform.position = player.position - direction / 4;

        canTeleport = false;
        Invoke("ResetCanTeleport", 2);
    }

    void ResetCanTeleport()
    {
        canTeleport = true;
    }

    private void StartChasingPlayer()
    {
        isActive = true;
    }

    private void AttackPlayer()
    {
        if (!isAttacking)
        {
            hitEffect.SetActive(true);
            Invoke("ResetHitEffect", .5f);

            //agent.Stop();

            playerData.health -= attackDamage;

            // Сбрасываем таймер атаки и устанавливаем состояние атаки
            attackTimer = attackDelay;
            isAttacking = true;
        }
    }

    void ResetHitEffect()
    {
        hitEffect.SetActive(false);
    }
}