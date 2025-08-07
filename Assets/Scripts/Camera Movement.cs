using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject map;
    private Transform tform;
    private Camera cam;
    [SerializeField] private float initialZoom;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    [SerializeField] private float zoomSpeed;

    void Start()
    {
        tform = GetComponent<Transform>();
        cam = GetComponent<Camera>();

        MapGeneration mapGen = map.GetComponent<MapGeneration>();
        int width = mapGen.width;
        int height = mapGen.height;

        //Starting Position
        tform.position = new Vector3((width / 2f) - 0.5f, (height / 2f) - 0.5f, -10f);

        //Set Zoom (5 default)
        cam.orthographicSize = initialZoom;
    }

    private Vector3 dragOrigin;

    void Update()
    {
        //Camera moves with middle mouse drag
        if (Input.GetMouseButtonDown(2))
        {
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 difference = dragOrigin - currentPos;

            transform.position += difference;
        }

        //Camera Zooms relative to mouse position
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scroll) > 0.01f)
        {
            Vector3 mouseWorldBeforeZoom = cam.ScreenToWorldPoint(Input.mousePosition);

            // Adjust zoom level
            cam.orthographicSize -= scroll * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);

            Vector3 mouseWorldAfterZoom = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 difference = mouseWorldBeforeZoom - mouseWorldAfterZoom;

            // Move camera to keep mouse position stationary relative to world
            cam.transform.position += difference;
        }
    }
}
