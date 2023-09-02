using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScene : MonoBehaviour
{
    AsyncOperation asyncOperation;
    //public Image LoadBar;
    public TextMeshProUGUI BarTxt;
    public int SceneID;

    private void Start()
    {
        StartCoroutine(LoadSceneCor());
    }


    IEnumerator LoadSceneCor()
    {
        yield return new WaitForSeconds(1f);
        asyncOperation = SceneManager.LoadSceneAsync(SceneID);
        while (!asyncOperation.isDone)
        {
            float progress = asyncOperation.progress / 0.9f;
            //LoadBar.fillAmount = progress;
            BarTxt.text = "loading..." + string.Format("{0:0}%", progress * 100f);
            yield return 0;
        }
    }

}