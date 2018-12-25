using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float lerpTime = 1;
    public Transform target;

    private Vector3 offset;
    private float lerpTimer = 0;

    void Start()
    {
        offset =  transform.position - target.position;
    }

    void Update()
    {
        if (transform.position != target.position + offset)
        {
            lerpTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpTimer / lerpTime);
        } else
        {
            lerpTimer = 0;
        }
    }
}
