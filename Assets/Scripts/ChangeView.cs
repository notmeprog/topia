using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using MoreMountains.Feedbacks;

public class ChangeView : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;

    [Header("KinoEffect")]

    [SerializeField] GameObject canvasKino;

    [SerializeField] Animator imageUpAnim;
    [SerializeField] Animator imageDownAnim;

    [Header("Камеры")]
    [SerializeField] GameObject cutsceneCamera;
    GameObject normalCamera;
    //CinemachineVirtualCamera cutsceneVirtualCamera;

    bool changeFOVCutscene = false;

    [Header("UI")]
    public GameObject[] uiElements;

    /*[Header("New Color")]
    [SerializeField] Light directionalLight;
    [SerializeField] Color newColor;
    Color startColor; */

    [Header("New Color")]
    [SerializeField] bool needChangeColor = true;
    [SerializeField] Volume volume;
    ColorAdjustments colorAdjustments;
    [SerializeField] int newHueShift;
    bool changeColor = false;

    [Header("Feedback")]
    [SerializeField] MMFeedbacks fovFeedback;

    private void Awake()
    {
        normalCamera = GameObject.FindGameObjectWithTag("CMcam1");
        virtualCamera = normalCamera.GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        //cutsceneVirtualCamera = cutsceneCamera.GetComponent<CinemachineVirtualCamera>();
        //startColor = directionalLight.color;

        volume.profile.TryGet(out colorAdjustments);
    }


    public void ChangeViewToAnother(Transform anotherObj)
    {
        canvasKino.SetActive(true);

        //changeFOVCutscene = true;

        changeColor = true;

        //virtualCamera.Follow = anotherObj.transform;
        virtualCamera.LookAt = anotherObj.transform;

        normalCamera.SetActive(false);
        cutsceneCamera.SetActive(true);

        fovFeedback?.PlayFeedbacks();

        for (int i = 0; i < uiElements.Length; i++)
            uiElements[i].SetActive(false);
    }


    public void ChangeViewToPlayer()
    {
        //cutsceneCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, 50, 10 * Time.deltaTime);

        changeColor = false;

        imageDownAnim.SetTrigger("Close");
        imageUpAnim.SetTrigger("Close");

        //virtualCamera.Follow = player.transform;
        virtualCamera.LookAt = null;

        normalCamera.SetActive(true);
        cutsceneCamera.SetActive(false);

        Invoke("CloseCanvasKino", 1.5f);
    }

    void CloseCanvasKino()
    {
        canvasKino.SetActive(false);

        for (int i = 0; i < uiElements.Length; i++)
            uiElements[i].SetActive(true);
    }

    void Update()
    {
        if (needChangeColor)
        {
            if (changeColor)
            {
                //directionalLight.color = Color.Lerp(directionalLight.color, newColor, 1 * Time.deltaTime);
                colorAdjustments.hueShift.value = Mathf.Lerp(colorAdjustments.hueShift.value, newHueShift, 1 * Time.deltaTime);
                //colorAdjustments.hueShift.value = newHueShift;
            }
            else
            {
                colorAdjustments.hueShift.value = Mathf.Lerp(colorAdjustments.hueShift.value, 0, 1 * Time.deltaTime);
                //directionalLight.color = Color.Lerp(directionalLight.color, startColor, 1 * Time.deltaTime);
            }
        }

        //if (changeFOVCutscene)
        //cutsceneVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(cutsceneVirtualCamera.m_Lens.FieldOfView, 45, 5 * Time.deltaTime);
    }
}
