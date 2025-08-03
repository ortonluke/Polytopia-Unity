using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject map;
    private Transform tform;
    [SerializeField] private float z;

    // Start is called before the first frame update
    void Start()
    {
        tform = GetComponent<Transform>();
        MapGeneration mapGen = map.GetComponent<MapGeneration>();
        int boardSize = mapGen.boardSize;

        tform.position = new Vector3(boardSize / 2f, boardSize / 2f, z * -1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
