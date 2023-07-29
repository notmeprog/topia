using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEyes : MonoBehaviour
{
    public GameObject eyes;

    void Start()
    {
        Invoke("OpenEyes", 5);
    }

    void OpenEyes()
    {
        eyes.SetActive(true);
    }
}
