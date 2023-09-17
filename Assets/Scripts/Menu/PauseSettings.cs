using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class PauseSettings : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    bool oneTime = false;
    public AudioSource audioExit;
    [SerializeField] private PauseMenuNavigation pauseMenuNavigation;
    [SerializeField] private PauseMenu pauseMenu;

    private void OnEnable()
    {
        oneTime = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !oneTime)
        {
            oneTime = true;
            audioExit.Play();
            StartCoroutine("NonActivePanel");
        }
    }

    IEnumerator NonActivePanel()
    {
        yield return new WaitForSecondsRealtime(.1f);
        pauseMenu.inSettings = false;
        pauseMenuNavigation.enabled = true;
        gameObject.SetActive(false);
    }
}
