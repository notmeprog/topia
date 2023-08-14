using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : MonoBehaviour
{
    [SerializeField] private PlayerMovementAdvanced playerMovementAdvancedSc;
    [SerializeField] private GameObject canvasDialog;

    [Header("Two cameras")]
    [SerializeField] private GameObject mainCam;
    [SerializeField] private GameObject cam2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("StartDialog");
        }
    }

    IEnumerator StartDialog()
    {
        playerMovementAdvancedSc.stopMoving = true;

        mainCam.SetActive(false);
        cam2.SetActive(true);

        yield return new WaitForSeconds(1);
        canvasDialog.SetActive(true);
    }
}
