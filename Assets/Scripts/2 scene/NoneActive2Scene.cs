using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoneActive2Scene : MonoBehaviour
{
    [SerializeField] private GameObject wallStart;
    [SerializeField] private GameObject triggerWall;

    private void Awake()
    {
        if (DifferentStatic.endBattle)
        {
            wallStart.SetActive(false);
            triggerWall.SetActive(false);
        }
    }
}
