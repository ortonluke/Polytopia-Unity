using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGeneration : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    public int boardSize;

    [Range(0, 100)]
    [SerializeField] private int iniChance;
    [Range(1, 8)]
    [SerializeField] private int birthLimit;
    [Range(1, 8)]
    [SerializeField] private int deathLimit;

    [Range(1, 10)]
    [SerializeField] private int numR;
    private int count = 0; //maybe clean this later?

    [SerializeField] private Vector3Int tmapSize;



    private int[,] GenerateMapData()
    {
        int[,] map = new int[boardSize, boardSize];

        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                /*
                 * 0 = Water (dead)
                 * 1 = Land (alive)
                 */

                // Example: Checkerboard pattern
                map[x, y] = Random.Range(1, 101) < iniChance ? 1 : 0;
            }
        }

        PrintMap(map);

        return map;
    }

    private void GenerateTiles()
    {
        int[,] map = GenerateMapData();

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                GameObject newTile = Instantiate(tile, new Vector2(x, y), Quaternion.identity);
                newTile.transform.SetParent(this.transform);
                Tile tileData = newTile.GetComponent<Tile>();

                if (map[x, y] == 1)
                {
                    tileData.SetTileType("Land");
                }
                else if (map[x, y] == 0)
                {
                    tileData.SetTileType("Water");
                }
            }
        }
    }

    private void PrintMap(int[,] map)
    {
        string output = "";
        for (int y = map.GetLength(1) - 1; y >= 0; y--)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                output += map[x, y].ToString() + " ";
            }
            output += "\n";
        }
        Debug.Log(output);
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateTiles();
    }
}
