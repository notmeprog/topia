using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string nameScene = "";

    public void LoadScene()
    {
        SceneManager.LoadScene(nameScene);
    }
}
