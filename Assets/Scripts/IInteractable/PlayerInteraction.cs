using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera mainCam;
    public float interactionDistance = 10;

    public GameObject interactionUI;

    public GameObject[] interactableObjects;


    void Update()
    {
        InteractionRay();
    }

    void InteractionRay()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;

        bool hitSomething = false;

        if (!hitSomething && interactableObjects.Length != 0)
        {
            for (int i = 0; i < interactableObjects.Length; i++)
                interactableObjects[i].layer = LayerMask.NameToLayer("Default");
        }

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                hitSomething = true;

                interactable.Highlight();

                if (Input.GetKeyDown(KeyCode.E))
                    interactable.Interact();
            }
        }

        interactionUI.SetActive(hitSomething);
    }
}
