using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Animator audioPitchAnim;
    [SerializeField] private AudioSource audioOpenClose;
    [SerializeField] private AudioSource audioClick;

    [Header("LoadScene")]
    [SerializeField] private string nameScene = "";
    [SerializeField] private GameObject panelEnd;
    [SerializeField] private PauseMenuNavigation pauseMenuNavigation;
    bool isPaused = false;

    [Header("Settings")]
    [SerializeField] private GameObject settingsPanel;
    public bool inSettings = false;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !inSettings && DifferentStatic.canOpenPauseMenu)
        {
            if (isPaused)
                Resume();
            else
                Pause();

            audioOpenClose.Play();
            
        }
    }

    void Pause()
    {
        audioPitchAnim.SetTrigger("PitchDown");
        Time.timeScale = 0f; // Замораживаем время
        pauseMenuUI.SetActive(true);
        isPaused = true;

        audioOpenClose.pitch = 1f;
    }

    public void Resume()
    {
        audioPitchAnim.SetTrigger("PitchUp");
        Time.timeScale = 1f; // Возобновляем время
        pauseMenuUI.SetActive(false);
        isPaused = false;

        audioOpenClose.pitch = .7f;
        audioClick.Play();
    }

    public void Options()
    {
        pauseMenuNavigation.enabled = false;
        settingsPanel.SetActive(true);
        audioClick.Play();

        inSettings = true;
    }

    public void MainMenu()
    {
        audioClick.Play();
        pauseMenuNavigation.enabled = false;
        panelEnd.SetActive(true);
        StartCoroutine("LoadScene");
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1f;
        SceneManager.LoadScene(nameScene);
    }
}
