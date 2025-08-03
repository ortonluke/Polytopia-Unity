using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject map;
    private Transform tform;
    private Camera cam;
    [SerializeField] private float initialZoom;
    void Start()
    {
        tform = GetComponent<Transform>();
        cam = GetComponent<Camera>();

        MapGeneration mapGen = map.GetComponent<MapGeneration>();
        int boardSize = mapGen.boardSize;

        //Starting Position
        tform.position = new Vector3((boardSize / 2f) - 0.5f, (boardSize / 2f) - 0.5f, -10f);

        //Set Zoom (5 default)
        cam.orthographicSize = initialZoom;
    }

    void Update()
    {
        
    }
}
