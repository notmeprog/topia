using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MoreMountains.Feedbacks;

public class WeaponReload : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private int maxAmmo = 0;
    public int ammoCount = 0;

    [SerializeField] private TextMeshProUGUI textBulletCount;
    //[SerializeField] private Image bulletImage;

    [Header("Feedback")]
    [SerializeField] private MMFeedbacks reloadFeedback;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip reloadSFX;

    [Header("ReloadText")]
    [SerializeField] GameObject reloadTextPopup;

    bool oneTime = false;

    private void OnEnable()
    {
        if (ammoCount == 0)
            DifferentStatic.openReloadGame = true;

        UpdateText();
    }

    /*private void Awake()
    {
        ammoCount = maxAmmo;
        UpdateText();
    }*/

    public void UpdateText()
    {
        reloadFeedback?.PlayFeedbacks();
        textBulletCount.text = ammoCount + "";
    }

    private void Update()
    {
        if (DifferentStatic.canReload != 0)
        {
            ReloadAmmo(DifferentStatic.canReload);
            DifferentStatic.canReload = 0;
        }

        OpenMiniGame();
    }

    public void ReloadAmmo(int bulletCount)
    {
        //Transform popupText = Instantiate(reloadTextPopup, gun.position, Quaternion.identity);
        //popupText.GetComponent<TextMeshPro>().SetText(bulletCount + "");

        reloadTextPopup.SetActive(true);
        reloadTextPopup.GetComponent<TextMeshPro>().SetText(bulletCount + "");
        Invoke("NoneActiveReloadText", 1);

        audioSource.clip = reloadSFX;
        audioSource.Play();

        ammoCount = bulletCount;
        UpdateText();
        oneTime = false;
    }

    void NoneActiveReloadText()
    {
        reloadTextPopup.SetActive(false);
    }

    void OpenMiniGame()
    {
        if (ammoCount == 0 && !oneTime)
        {
            DifferentStatic.openReloadGame = true;
            oneTime = true;
        }
    }
}
