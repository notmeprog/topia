using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenuNavigation : MonoBehaviour
{
    public Button[] buttons;
    public TextMeshProUGUI[] textMeshProUGUIs;
    public Color normalColor; // Обычный цвет кнопки
    public Color selectedColor; // Цвет для выделенной кнопки

    [SerializeField] private AudioSource audioSource;

    private int currentButtonIndex = 0;

    private void Start()
    {
        SelectButton(currentButtonIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            SwitchButton(-1);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            SwitchButton(1);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))        //это если что убрать
            buttons[currentButtonIndex].onClick.Invoke();
    }

    private void SwitchButton(int direction)
    {
        if ((currentButtonIndex + direction) < buttons.Length)
        {
            SelectButton(currentButtonIndex + direction);

            audioSource.Play();
        }
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
