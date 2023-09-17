using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI;
using TMPro;
using Honeti;

public class DialogTypewrite : MonoBehaviour
{
    [Header("Локализация")]
    public I18NTextMesh i18NTextMesh;

    [Space(10)]
    [SerializeField] TextMeshProUGUI textDialog;
    private PlayerMovementAdvanced playerMovementAdvanced;

    [Header("Диалог")]
    public string[] strings;
    // public string[] addStrings;


    public bool needCircle = true;
    [SerializeField] private Animator circleTime;

    int numberString = 0;
    public int NumberString => numberString;

    Animator dlgBoxAnim;

    [Header("Время ожидания")]
    [SerializeField] float timeWait = 3f;

    const int startValue = 19;

    public bool endDialog = false;

    public bool activeAnswers = false;

    [Header("Нужно игрока тормозить?")]

    public bool needStopPlayer = false;



    void Awake()
    {
        playerMovementAdvanced = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementAdvanced>();
        dlgBoxAnim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        if (needStopPlayer)
            playerMovementAdvanced.stopMoving = true;

        numberString = 0;
        textDialog.text = strings[0];
        i18NTextMesh.updateDialogText(strings[numberString]);
    }

    private void Update()
    {
        if (!endDialog)
            CloseDialog();
    }

    public void EndLine()
    {
        print("end of line");
        if (!activeAnswers)
        {
            Invoke("NextString", timeWait);

            if (needCircle)
                circleTime.SetTrigger("Start");
        }
    }

    public void NextString()
    {
        numberString++;
        textDialog.text = strings[numberString];

        i18NTextMesh.updateDialogText(strings[numberString]);

        print(strings[numberString]);

        if (needCircle)
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
        if (needStopPlayer)
            playerMovementAdvanced.stopMoving = false;

        yield return new WaitForSeconds(timeWait + 1);
        if (!endDialog)
            dlgBoxAnim.SetTrigger("EndDialog");

        endDialog = true;
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
