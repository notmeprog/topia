using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNewLevel : MonoBehaviour
{
    PlayerMovementAdvanced playerMovementAdvanced;
    Shotgun shotgunSc;

    void Start()
    {
        playerMovementAdvanced = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementAdvanced>();


        playerMovementAdvanced.stopMoving = false;

        playerMovementAdvanced.isLoadingLevel = false;

        shotgunSc = FindObjectOfType<Shotgun>();

        if (shotgunSc != null)
            shotgunSc.canShoot = true;
    }
}
