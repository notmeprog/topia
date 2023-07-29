using Cinemachine;
using UnityEngine;
using MoreMountains.Feedbacks;

public class InteractFar : MonoBehaviour
{
    public Camera mainCam;
    public float interactionDistance = 50;
    public float fovValue = 20;

    [SerializeField] Animator krotAnim;
    [SerializeField] Animator owlAnim;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    [Header("OwlEffect")]
    [SerializeField] MMFeedbacks owlFeedback;

    bool oneTime = false;

    void Update()
    {
        InteractionRay();
    }

    void InteractionRay()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            print(hit.collider.name);
            if (hit.collider.tag == "InteractFar" && virtualCamera.m_Lens.FieldOfView < fovValue)
            {
                KrotHide();
            }
            else if (hit.collider.tag == "OWLInteract" && oneTime == false)
            {
                OWLHide();
            }
        }
    }

    void KrotHide()
    {
        krotAnim.GetComponent<BoxCollider>().enabled = false;

        krotAnim.SetTrigger("Hide");
    }

    void OWLHide()
    {
        oneTime = true;
        owlFeedback?.PlayFeedbacks();

        owlAnim.GetComponent<BoxCollider>().enabled = false;

        owlAnim.SetTrigger("Hide");
    }
}