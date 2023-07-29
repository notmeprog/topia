using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class EnemyMain : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] MMFeedbacks damageFeedback;
    [SerializeField] GameObject particleDead;

    [Header("AnimHit")]
    [SerializeField] SpriteRenderer spriteRenderer;

    public void TakeDamage(int damage)
    {
        health -= damage;

        damageFeedback?.PlayFeedbacks();

        spriteRenderer.color = Color.red;
        Invoke("ChangeColorHit", .5f);

        if (health <= 0)
            DeathEnemy();
    }

    void ChangeColorHit()
    {
        spriteRenderer.color = Color.white;
    }

    void DeathEnemy()
    {
        Instantiate(particleDead, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
