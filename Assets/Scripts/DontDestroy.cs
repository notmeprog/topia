using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    DontDestroy[] dontDestroyObjs;

    void Start()
    {
        dontDestroyObjs = Object.FindObjectsOfType<DontDestroy>();

        for (int i = 0; i < dontDestroyObjs.Length; i++)
        {
            if (dontDestroyObjs[i] != this)
            {
                if (dontDestroyObjs[i].name == gameObject.name)
                {
                    Destroy(gameObject);
                }
            }
        }

        DontDestroyOnLoad(gameObject);
    }
}
