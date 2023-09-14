using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressedBridge : MonoBehaviour
{
    [SerializeField] private SecondBridgeButton secondBridgeButton;
    [SerializeField] private ButtonBridge buttonBridge;
    [SerializeField] Animator bridgeAnim;

    void Update()
    {
        //if (secondBridgeButton.isPressed && buttonBridge.isPressed)
        //bridgeAnim.SetTrigger("bridgeDown");
        //else bridgeAnim.SetTrigger("bridgeUp");
    }

    public void BridgeDown()
    {
        bridgeAnim.SetTrigger("bridgeDown");
        DifferentStatic.bridgeOpen = true;
    }

    public void BridgeUp()
    {
        bridgeAnim.SetTrigger("bridgeUp");
        DifferentStatic.bridgeOpen = false;
    }
}
