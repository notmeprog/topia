using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;

public class KeyboardNavigation : MonoBehaviour
{
    [Header("Возможность выбирать кнопку для ответа")]
    public bool canChoice = false;

    public Button button1;
    public Button button2;

    public Image outlineImage; // Присвойте это через инспектор

    private Button selectedButton;

    [Header("Camera")]
    public bool needCameraMove = false;
    public Animator cameraMove;

    [Header("UI Effect")]
    [SerializeField] MMFeedbacks swtchFeedback;

    private void Start()
    {
        selectedButton = button1;
        selectedButton.Select();
    }

    private void Update()
    {
        //CameraMove();
        if (canChoice)
        {
            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
            {
                if (selectedButton == button1)
                {
                    selectedButton = button2;
                    cameraMove.SetTrigger("RightMove");
                }
                else
                {
                    selectedButton = button1;
                    cameraMove.SetTrigger("LeftMove");
                }

                UpdateOutlinePosition();
                selectedButton.Select();
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
            {
                if (selectedButton == button1)
                {
                    selectedButton = button2;
                    cameraMove.SetTrigger("RightMove");
                }
                else
                {
                    selectedButton = button1;
                    cameraMove.SetTrigger("LeftMove");
                }

                UpdateOutlinePosition();
                selectedButton.Select();

            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                selectedButton.onClick.Invoke();
                canChoice = false;
            }
        }
    }

    private void UpdateOutlinePosition()
    {
        outlineImage.rectTransform.position = selectedButton.transform.position;

        swtchFeedback?.PlayFeedbacks();
    }

    void CameraMove()
    {
        if (!needCameraMove)
            return;

        if (selectedButton == button2)
            cameraMove.SetTrigger("RightMove");
        else if (selectedButton == button1)
            cameraMove.SetTrigger("LeftMove");
    }
}
