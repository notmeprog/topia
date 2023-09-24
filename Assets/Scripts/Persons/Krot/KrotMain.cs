
using System.Collections;
using UnityEngine;
using MoreMountains.Feedbacks;

public class KrotMain : MonoBehaviour
{
    public int health = 100;
    int maxHealth;

    [SerializeField] MMFeedbacks damageFeedback;

    [SerializeField] GameObject healthBar;
    HealthBar healthBarSc;

    [SerializeField] Animator krotAnim;

    [Header("Not dead")]
    [SerializeField] MMFeedbacks deadFeedback;
    [SerializeField] GameObject ghostObj;
    [SerializeField] GameObject kaskaObj;
    [SerializeField] GameObject krotSprite;
    public bool isDead = false;
    public bool IsDead => isDead;

    [Header("Пощада")]
    [SerializeField] GameObject whiteFlag;
    [SerializeField] GameObject weapon;
    public bool isMercy = false;
    public bool isMercyEscape = false;

    bool isActive = true;
    public bool IsActive => isActive;


    void Start()
    {
        maxHealth = health;

        healthBarSc = healthBar.GetComponent<HealthBar>();

        healthBarSc.SetMaxHealth(health);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            SadDeath();
        else if (health <= maxHealth * 0.1f)
            Mercy();

        ActiveSlider();

        damageFeedback?.PlayFeedbacks();
    }

    void ActiveSlider()
    {
        healthBar.SetActive(true);

        healthBarSc.SetHealth(health);
        Invoke("NoneActiveSlider", 1.5f);
    }

    void NoneActiveSlider()
    {
        if (healthBarSc.IsActive)
            return;

        healthBar.SetActive(false);
    }

    void Mercy()
    {
        isActive = false;

        weapon.SetActive(false);
        //weapon.GetComponent<Animator>().SetTrigger("End");

        isMercy = true;
        whiteFlag.SetActive(true);
        StartCoroutine("Escape");
    }

    IEnumerator Escape()
    {
        yield return new WaitForSeconds(7);
        if (!isDead)
        {
            isMercyEscape = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            weapon.SetActive(false);

            krotAnim.enabled = true;
            krotAnim.SetTrigger("Escape");
            yield return new WaitForSeconds(.7f);
            gameObject.SetActive(false);
        }
    }

    void SadDeath()
    {
        isDead = true;

        deadFeedback?.PlayFeedbacks();

        weapon.SetActive(false);
        whiteFlag.SetActive(false);
        krotSprite.SetActive(false);
        ghostObj.SetActive(true);
        kaskaObj.SetActive(true);

        gameObject.SetActive(false);
    }
}
