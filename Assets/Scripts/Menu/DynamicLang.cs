using Honeti;
using UnityEngine;
using TMPro;

public class DynamicLang : MonoBehaviour
{
    public I18NTextMesh[] i18NTextMesh;
    public TextMeshProUGUI[] textDialog;


    public void ChangeTextLanguage()
    {
        for (int i = 0; i < i18NTextMesh.Length; i++)
        {
            i18NTextMesh[i].updateDialogText(textDialog[i].text);
        }
    }
}
