using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MehaMovement : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;

    private Rigidbody rb;
    Vector3 direction;
    float distance;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        direction = player.position - transform.position;
        direction.Normalize();
    }

    public void ShakeCamera()
    {
        if (distance < 2)
            CameraShake.Instance.ShakeCamera(5f, 0.15f, 1);
        else if (distance < 5 && distance >= 2)
            CameraShake.Instance.ShakeCamera(4f, 0.15f, 1);
        else if (distance < 10 && distance >= 5)
            CameraShake.Instance.ShakeCamera(3.5f, 0.15f, 1);
    }

    private void FixedUpdate()
    {
        distance = Vector3.Distance(transform.position, player.position);

        Vector3 newPosition = transform.position + direction * moveSpeed * Time.fixedDeltaTime;

        // Перемещаем робота к новой позиции
        //rb.MovePosition(newPosition);
    }
}
