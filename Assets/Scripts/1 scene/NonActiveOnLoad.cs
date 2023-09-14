using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonActiveOnLoad : MonoBehaviour
{
    [SerializeField] private GameObject rifleInteract;
    [SerializeField] private GameObject backInteract;
    [SerializeField] private GameObject krotFar;
    [SerializeField] private GameObject owlSee;

    [SerializeField] private GameObject owlShadow;
    [SerializeField] private PressedBridge pressedBridgeSc;
    [SerializeField] private Transform cubeGreen;
    [SerializeField] private Transform cubePoint;

    void Start()
    {
        if (DifferentStatic.isWeaponPickup)
            rifleInteract.SetActive(false);

        if (DifferentStatic.isBackInteract)
            backInteract.SetActive(false);

        if (DifferentStatic.playerSeeKrot)
            krotFar.SetActive(false);

        if (DifferentStatic.playerSeeOwl)
            owlSee.SetActive(false);

        if (DifferentStatic.isOwlShadowInteract)
            owlShadow.SetActive(false);

        if (DifferentStatic.bridgeOpen)
        {
            pressedBridgeSc.BridgeDown();
            cubeGreen.position = cubePoint.position;
        }
    }
}
