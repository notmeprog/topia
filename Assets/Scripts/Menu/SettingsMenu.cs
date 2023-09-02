using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using MoreMountains.Feedbacks;

public class SettingsMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    public AudioMixer audioMixer;

    [SerializeField] private GameObject menuButtons;
    bool oneTime = false;
    [Header("Effect")]
    [SerializeField] MMFeedbacks exitFeedback;

    private void OnEnable()
    {
        oneTime = false;
        Cursor.visible = true;
        menuButtons.SetActive(false);
    }

    private void OnDisable()
    {
        Cursor.visible = false;
        menuButtons.SetActive(true);
    }

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
        }

        resolutionDropdown.AddOptions(options);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SfxVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetShadows(bool isShadows)
    {
        if (isShadows)
            QualitySettings.shadows = ShadowQuality.All;
        else
            QualitySettings.shadows = ShadowQuality.Disable;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !oneTime)
        {
            oneTime = true;
            exitFeedback?.PlayFeedbacks();
            Invoke("NonActivePanel", .3f);
        }
    }

    void NonActivePanel()
    {
        gameObject.SetActive(false);
    }
}
