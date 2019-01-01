using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float lerpTime;
    public Transform target;

    private Vector3 verticalOffset;
    private Vector3 horizontalOffset;
    private float lerpTimer = 0;

    public float zoomAmount;
    public float maxZoomIn;
    public float maxZoomOut;
    public float zoomSpeed;

    public float scrollSpeed = 5;

    void Start()
    {
        verticalOffset =  transform.position - target.position;
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            zoomAmount += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            zoomAmount = Mathf.Clamp(zoomAmount, maxZoomOut, maxZoomIn);
            verticalOffset = transform.forward * zoomAmount;
        }

        if (Input.mousePosition.x >= Screen.width * 0.95)
        {
            horizontalOffset += Vector3.right * Time.deltaTime * scrollSpeed;
        } else if (Input.mousePosition.x <= Screen.width * 0.05)
        {
            horizontalOffset -= Vector3.right * Time.deltaTime * scrollSpeed;
        }

        if (Input.mousePosition.y >= Screen.height * 0.95)
        {
            horizontalOffset += Vector3.forward * Time.deltaTime * scrollSpeed;
        }
        else if (Input.mousePosition.y <= Screen.height * 0.05)
        {
            horizontalOffset -= Vector3.forward * Time.deltaTime * scrollSpeed;
        }

        if (Input.GetMouseButtonDown(2))
        {
            horizontalOffset = new Vector3(0, 0, 0);
        }

        if (transform.position != target.position + verticalOffset + horizontalOffset)
        {
            lerpTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, target.position + verticalOffset + horizontalOffset, lerpTimer / lerpTime);
        } else
        {
            lerpTimer = 0;
        }
    }
}
