using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGeneration : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    public int width;
    public int height;

    [Range(0, 100)]
    [SerializeField] private int iniChance;
    [Range(1, 8)]
    [SerializeField] private int birthLimit;
    [Range(1, 8)]
    [SerializeField] private int deathLimit;

    [Range(1, 10)]
    [SerializeField] private int numSim;

    [SerializeField] private Vector3Int tmapSize;

    private void GenerateTiles()
    {
        int[,] map = GenerateMapData();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
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

    private int[,] GenerateMapData()
    {
        /*
        * 0 = Water (dead)
        * 1 = Land (alive)
        */
        
        int[,] map = new int[width, height];

        //Initial Position
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                
                map[x, y] = Random.Range(1, 101) < iniChance ? 1 : 0;
            }
        }

        //Run Simulations
        for (int i = 0; i < numSim; i++)
        {
            map = simMap(map);
        }

        PrintMap(map);

        return map;
    }

    private int[,] simMap(int[,] oldMap)
    {
        int[,] newMap = new int[width, height];
        int neighb;
        BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighb = 0;
                foreach (var b in myB.allPositionsWithin)
                {
                    if (b.x == 0 && b.y == 0) continue;
                    if (x + b.x >= 0 && x + b.x < width && y + b.y >= 0 && y + b.y < height)
                    {
                        neighb += oldMap[x + b.x, y + b.y];
                    }
                    else
                    {
                        neighb++;
                    }

                }

                if (oldMap[x, y] == 1)
                {
                    if (neighb < deathLimit) newMap[x, y] = 0;


                    else
                    {
                        newMap[x, y] = 1;


                    }
                }

                if (oldMap[x, y] == 0)
                {
                    if (neighb > birthLimit) newMap[x, y] = 1;


                    else
                    {
                        newMap[x, y] = 0;
                    }

                }
            }


            
        }
        return newMap;
    }

    

    private void PrintMap(int[,] map)
    {
        string output = "";
        for (int y = height - 1; y >= 0; y--)
        {
            for (int x = 0; x < width; x++)
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

    private void Update()
    {

    }
}
