using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraToPlayer : MonoBehaviour
{
    Transform player;
    public Vector3 newPosition = new Vector3(-3.973642e-08f, 0.4689999f, 0.2910054f);

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void OnEnable()
    {
        transform.SetParent(player);
        transform.localPosition = newPosition;
    }
}
