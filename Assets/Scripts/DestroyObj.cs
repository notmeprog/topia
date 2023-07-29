using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    [SerializeField] private float timer = 1;
    void Start()
    {
        Destroy(gameObject, timer);
    }
}
