using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    [SerializeField] private float xDegrees = 20;
    [SerializeField] private float yDegrees = 20;
    [SerializeField] private float zDegrees = 20;
    public bool isRotate = true;

    private void Update()
    {
        if (isRotate)
            transform.Rotate(new Vector3(xDegrees, yDegrees, zDegrees) * Time.deltaTime);
    }
}
