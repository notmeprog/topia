using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreezer : MonoBehaviour
{
    public void FreezeTime(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(SoTimeFreeze(duration));
    }

    IEnumerator SoTimeFreeze(float duration)
    {
        print("yop");
        Time.timeScale = .1f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;
    }
}
