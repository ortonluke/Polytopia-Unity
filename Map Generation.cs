using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGeneration : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    public int boardSize;

    private int[,] GenerateMapData(int boardSize)
    {
        int[,] map = new int[boardSize, boardSize];

        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                // Example: Checkerboard pattern
                map[x, y] = (x + y) % 2 == 0 ? 0 : 1;
            }
        }

        return map;
    }

    private void GenerateTiles()
    {
        for (float x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                GameObject newTile = Instantiate(tile, new Vector2(x, y), Quaternion.identity);
                newTile.transform.SetParent(this.transform);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
