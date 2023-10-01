using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Honeti;

public class ChangeLang : MonoBehaviour
{
    private void Awake()
    {
        LanguageCode languageCode = (LanguageCode)DifferentStatic.languageIndex;

        I18N.instance._defaultLang = languageCode;
    }
}
