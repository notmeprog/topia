using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public Transform[] waypoints; // Массив точек, по которым будут перемещаться жители
    public float minWaitTime = 2f; // Минимальное время ожидания в каждой точке
    public float maxWaitTime = 5f; // Максимальное время ожидания в каждой точке

    private NavMeshAgent agent;
    private int currentWaypointIndex;
    private bool isMoving;
    private float waitTime;

    public SpriteRenderer spriteRenderer; // Ссылка на компонент SpriteRenderer для изменения спрайта жителя
    public Sprite spriteFront; // Спрайт, когда житель смотрит на игрока
    public Sprite spriteBack; // Спрайт, когда житель повернут спиной к игроку
    public Transform playerTransform;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentWaypointIndex = 0;
        isMoving = true;
        waitTime = Random.Range(minWaitTime, maxWaitTime);

        MoveToNextWaypoint();
    }

    private void Update()
    {
        if (isMoving)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude <= 0.1f)
                {
                    // Житель достиг текущей точки, начинаем ожидание
                    isMoving = false;
                    Invoke("MoveToNextWaypoint", waitTime);
                }
            }
        }

        UpdateSpriteDirection();
    }

    private void MoveToNextWaypoint()
    {
        // Переходим к следующей точке в массиве
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

        // Задаем новую цель для NavMeshAgent
        agent.SetDestination(waypoints[currentWaypointIndex].position);

        // Начинаем движение к новой цели
        isMoving = true;
        waitTime = Random.Range(minWaitTime, maxWaitTime);
    }

    private void UpdateSpriteDirection()
    {
        if (playerTransform != null)
        {
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            float angle = Mathf.Atan2(directionToPlayer.x, directionToPlayer.z) * Mathf.Rad2Deg;
            print(angle);

            // Определяем, в какой полуторе разворотов находится житель
            if (angle >= -135f && angle < -45f)
            {
                // Житель повернут спиной к игроку
                spriteRenderer.sprite = spriteBack;
            }
            else
            {
                // Житель смотрит на игрока
                spriteRenderer.sprite = spriteFront;
            }
        }
    }
}