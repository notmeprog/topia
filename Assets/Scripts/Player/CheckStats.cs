using MoreMountains.Feedbacks;
using UnityEngine;
using TMPro;

public class CheckStats : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private MMFeedbacks hitFeedback;
    [SerializeField] private TextMeshProUGUI textHealth;
    [SerializeField] private GameObject panelDeath;

    private int health;

    void OnEnable()
    {
        health = playerData.health;
        textHealth.text = playerData.health + "";
    }

    void Update()
    {
        //health = playerData.health;
        //print(playerData.health + "");

        if (health <= 0)
        {
            health = 101;   //если тут менять и playerdata, то ошибки не будет

            panelDeath.SetActive(true);
        }

        if (health != playerData.health)
        {
            hitFeedback?.PlayFeedbacks();
            textHealth.text = playerData.health + "";
            health = playerData.health;

            CameraShake.Instance.ShakeCamera(5f, 0.2f, 1);
        }
    }
}
