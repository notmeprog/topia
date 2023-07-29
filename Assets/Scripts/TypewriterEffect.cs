using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textDialog;

    [Header("Dialog")]
    [SerializeField] string[] strings;
    [SerializeField] string[] addStrings;

    int numberString = 0;
    [SerializeField] GameObject dlgBox;
    Animator dlgBoxAnim;


    [Header("ChangeView")]
    [SerializeField] ChangeView changeViewSc;

    [Header("Player")]
    [SerializeField] PlayerMovementAdvanced playerMovementAdvancedSc;

    [Header("InteractObj")]
    [SerializeField] GameObject interactObject;



    const int startValue = 19;

    void Awake()
    {
        dlgBoxAnim = dlgBox.GetComponent<Animator>();
    }

    void OnEnable()
    {
        numberString = 0;
        textDialog.text = strings[0];

        playerMovementAdvancedSc.stopMoving = true;
        //playerMovement._speed = 0;
    }

    private void Update()
    {
        //transform.position = mainCamera.WorldToScreenPoint(objAttach.position + offset);

        if (Input.GetKeyUp(KeyCode.E))
        {
            CloseDialog();

            NextString();
        }
    }

    void CloseDialog()
    {
        if (numberString != strings.Length - 1)
            return;

        dlgBoxAnim.SetTrigger("EndDialog");

        playerMovementAdvancedSc.stopMoving = false;
        //playerMovement._speed = 1;

        Invoke("NoneActiveObj", 0.4f);
    }

    void NoneActiveObj()
    {
        changeViewSc.ChangeViewToPlayer();

        if (interactObject != null)
            interactObject.GetComponent<IInteractable>().AfterInteract();

        dlgBox.SetActive(false);
    }

    void NextString()
    {
        if (textDialog.text.Length + addStrings[numberString].Length != strings[numberString].Length + startValue || numberString >= strings.Length - 1)
            return;


        print("next");
        numberString++;
        textDialog.text = strings[numberString];
    }

    void SkipText()
    {
        //крч текст набирается не постепенно, а вставляется готовым сразу
        if (textDialog.text.Length == strings[numberString].Length + startValue)
            return;

        //textAnimatorPlayer.SkipTypewriter();
    }
}
