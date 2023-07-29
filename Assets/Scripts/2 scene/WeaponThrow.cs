using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class WeaponThrow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float throwDuration = 1f; // Длительность броска в секундах
    [SerializeField] GameObject realWeapon;
    [SerializeField] TriggerWall triggerWallSc;
    [SerializeField] MMFeedbacks hitFeedback;

    [SerializeField] Animator cubeLookAt;

    private bool startMove = true;
    private Animator animator;
    private float throwTimer = 0f; // Таймер для отслеживания времени броска

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;

        StartCoroutine("EnableRealGun");
    }

    private void Update()
    {
        if (startMove)
        {
            // Увеличиваем таймер каждый кадр
            throwTimer += Time.deltaTime;

            // Вычисляем прогресс броска в диапазоне от 0 до 1
            float throwProgress = Mathf.Clamp01(throwTimer / throwDuration);

            // Интерполируем позицию от начальной до конечной позиции с заданной длительностью
            transform.position = Vector3.Lerp(transform.position, player.position + new Vector3(0, 0.3f, 0), throwProgress);

            // Если бросок завершен
            if (throwProgress >= 1f)
            {
                startMove = false;
            }
        }
    }

    IEnumerator EnableRealGun()
    {
        yield return new WaitForSeconds(.2f);
        cubeLookAt.SetTrigger("Hit");
        hitFeedback?.PlayFeedbacks();
        CameraShake.Instance.ShakeCamera(5f, 2f, 1);

        yield return new WaitForSeconds(.4f);

        triggerWallSc.ResetAfterCameraKrot();
        realWeapon.SetActive(true);
    }
}
