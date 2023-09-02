using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MoreMountains.Feedbacks;

public class MenuButtonNavigation : MonoBehaviour
{
    public Button[] buttons;
    public TextMeshProUGUI[] textMeshProUGUIs;
    public Color normalColor; // Обычный цвет кнопки
    public Color selectedColor; // Цвет для выделенной кнопки

    private int currentButtonIndex = 0;

    [Header("Effect")]
    [SerializeField] MMFeedbacks switchFeedback;

    private void Start()
    {
        SelectButton(currentButtonIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SwitchButton(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            SwitchButton(1);
        }
    }

    private void SwitchButton(int direction)
    {
        SelectButton(currentButtonIndex + direction);

        switchFeedback?.PlayFeedbacks();
    }

    private void SelectButton(int newIndex)
    {
        if (newIndex < 0)
        {
            newIndex = buttons.Length - 1;
        }
        else if (newIndex >= buttons.Length)
        {
            newIndex = 0;
        }

        textMeshProUGUIs[currentButtonIndex].color = normalColor;
        currentButtonIndex = newIndex;
        textMeshProUGUIs[currentButtonIndex].color = selectedColor;
        buttons[currentButtonIndex].Select();
    }
}
