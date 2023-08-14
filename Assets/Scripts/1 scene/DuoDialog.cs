using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI;
using TMPro;

public class DuoDialog : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textDialog;

    [Header("Dialog")]
    [SerializeField] string[] strings;
    [SerializeField] string[] addStrings;

    int numberString = 0;
    [SerializeField] GameObject dlgBox;
    //Animator dlgBoxAnim;

    [Header("ChangeFont")]
    [SerializeField] private TMP_FontAsset font1;
    [SerializeField] private TMP_FontAsset font2;

    [Header("ChangeAudio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip1;
    [SerializeField] private AudioClip clip2;

    const int startValue = 19;

    void Awake()
    {
        //dlgBoxAnim = dlgBox.GetComponent<Animator>();
    }

    void OnEnable()
    {
        numberString = 0;
        textDialog.text = strings[0];
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            ChangeFont();

            CloseDialog();

            NextString();
        }
    }

    void ChangeFont()
    {
        if (numberString == 0)
        {
            textDialog.font = font2;
            audioSource.clip = clip2;
        }
    }

    void CloseDialog()
    {
        if (numberString != strings.Length - 1)
            return;

        //dlgBoxAnim.SetTrigger("EndDialog");

        Invoke("NoneActiveObj", 0.4f);
    }

    void NoneActiveObj()
    {
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
