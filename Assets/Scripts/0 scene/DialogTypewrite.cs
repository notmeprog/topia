using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI;
using TMPro;

public class DialogTypewrite : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textDialog;

    [Header("Диалог")]
    [SerializeField] string[] strings;
    [SerializeField] string[] addStrings;

    [SerializeField] private Animator circleTime;

    int numberString = 0;

    Animator dlgBoxAnim;

    [Header("Время ожидания")]
    [SerializeField] float timeWait = 3f;

    const int startValue = 19;

    void Awake()
    {
        dlgBoxAnim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        numberString = 0;
        textDialog.text = strings[0];
    }

    private void Update()
    {
        CloseDialog();
    }

    public void EndLine()
    {
        Invoke("NextString", timeWait);

        circleTime.SetTrigger("Start");
    }

    void NextString()
    {
        numberString++;
        textDialog.text = strings[numberString];

        circleTime.SetTrigger("Idle");
    }


    void CloseDialog()
    {
        if (numberString != strings.Length - 1)
            return;

        StartCoroutine("NoneActiveObj");
    }

    IEnumerator NoneActiveObj()
    {
        yield return new WaitForSeconds(timeWait + 1);
        dlgBoxAnim.SetTrigger("EndDialog");
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
