using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 10f; // Скорость зума
    [SerializeField] private float minFOV = 10f; // Минимальный FOV
    [SerializeField] private float maxFOV = 100f; // Максимальный FOV

    [SerializeField] private Slider slider;

    private CinemachineVirtualCamera virtualCamera; // Ссылка на камеру

    [Header("SFX")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip zoomIn;
    [SerializeField] private AudioClip zoomOut;

    private bool isZoomingIn = false;
    private bool isZoomingOut = false;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>(); // Получаем компонент камеры

        slider.value = virtualCamera.m_Lens.FieldOfView;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Проверяем нажата ли левая кнопка мыши
        {
            isZoomingIn = true;
            audioSource.clip = zoomIn;
            audioSource.Play();
        }
        else if (Input.GetMouseButtonDown(1)) // Проверяем нажата ли правая кнопка мыши
        {
            isZoomingOut = true;
            audioSource.clip = zoomOut;
            audioSource.Play();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isZoomingIn = false;
            audioSource.Stop();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isZoomingOut = false;
            audioSource.Stop();
        }

        if (isZoomingIn) // Если левая кнопка мыши нажата, увеличиваем FOV
        {
            ZoomIn();
        }
        else if (isZoomingOut) // Если правая кнопка мыши нажата, уменьшаем FOV
        {
            ZoomOut();
        }
    }

    private void ZoomIn()
    {
        if (virtualCamera.m_Lens.FieldOfView > minFOV) // Проверяем, что текущий FOV больше минимального значения
        {
            virtualCamera.m_Lens.FieldOfView -= zoomSpeed * Time.deltaTime; // Уменьшаем FOV
            slider.value = virtualCamera.m_Lens.FieldOfView;
        }
    }

    private void ZoomOut()
    {
        if (virtualCamera.m_Lens.FieldOfView < maxFOV) // Проверяем, что текущий FOV меньше максимального значения
        {
            virtualCamera.m_Lens.FieldOfView += zoomSpeed * Time.deltaTime; // Увеличиваем FOV
            slider.value = virtualCamera.m_Lens.FieldOfView;
        }
    }
}