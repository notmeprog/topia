using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject panelEnd;
    [SerializeField] private GameObject settingsPanel;
    [Header("Effect")]
    [SerializeField] MMFeedbacks selectFeedback;
    [SerializeField] private Animator pitchAudio;

    private void Start()
    {
        Cursor.visible = false;
    }

    public void Play()
    {
        selectFeedback?.PlayFeedbacks();
        Invoke("PlayGame", .4f);
    }

    void PlayGame()
    {
        pitchAudio.SetTrigger("Close");
        panelEnd.SetActive(true);
    }

    public void OpenSettings()
    {
        selectFeedback?.PlayFeedbacks();
        Invoke("Settings", .3f);
    }

    void Settings()
    {
        settingsPanel.SetActive(true);
    }

    public void Exit()
    {
        selectFeedback?.PlayFeedbacks();
        Invoke("QuitGame", .3f);
    }

    void QuitGame()
    {
        pitchAudio.SetTrigger("Close");
        Application.Quit();
    }
}
