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
    [SerializeField] private int landBirthLimit;
    [Range(1, 8)]
    [SerializeField] private int landDeathLimit;
    [Range(1, 10)]
    [SerializeField] private int numSimLand;

    [Range(1, 100)]
    [SerializeField] private int mountainChance;
    [Range(1, 8)]
    [SerializeField] private int mounBirthLimit;
    [Range(1, 8)]
    [SerializeField] private int mounDeathLimit;
    [Range(1, 10)]
    [SerializeField] private int numSimMountain;

    [SerializeField] private BuildCreating buildCreator;

    private int[,] map;

    //[SerializeField] private Vector3Int tmapSize;
    private void GenerateTiles()
    {
        map = GenerateMapData(iniChance, landBirthLimit, landDeathLimit);
        int[,] mountainMap = GenerateMapData(mountainChance, mounBirthLimit, mounDeathLimit);

        //Merge Maps
        map = MergeMaps(map, mountainMap, 2);

        //Generate Key Structures
        buildCreator.GenerateCapital();

        PrintMap(map);

        //Create Tiles
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject newTile = Instantiate(tile, new Vector2(x, y), Quaternion.identity);
                newTile.transform.SetParent(this.transform);
                Tile tileData = newTile.GetComponent<Tile>();

                if (map[x, y] == 1)
                {
                    tileData.SetTileType("Land"); //1 = Land
                }
                else if (map[x, y] == 0)
                {
                    tileData.SetTileType("Water"); //0 = Water
                }
                else if (map[x,y] == 2)
                {
                    tileData.SetTileType("Mountain"); //2 = Mountain
                }
            }
        }
    }

    private int[,] GenerateMapData(int chance, int birthlimit, int deathlimit)
    {
        /*
        * 0 = Dead
        * 1 = Alive
        */
        
        int[,] map = new int[width, height];

        //Initial Position
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = Random.Range(1, 101) < chance ? 1 : 0;
            }
        }

        //Run Simulations
        for (int i = 0; i < numSimLand; i++)
        {
            map = simMap(map, landBirthLimit, landDeathLimit);
        }

        PrintMap(map);

        return map;
    }

    private int[,] simMap(int[,] oldMap, int birthlimit, int deathlimit)
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
                    if (neighb < deathlimit)
                    {
                        newMap[x, y] = 0;
                    }

                    else
                    {
                        newMap[x, y] = 1;
                    }
                }

                if (oldMap[x, y] == 0)
                {
                    if (neighb > birthlimit)
                    {
                        newMap[x, y] = 1;
                    }

                    else
                    {
                        newMap[x, y] = 0;
                    }
                }
            }
        }
        return newMap;
    }

    private int[,] MergeMaps(int[,] map1, int[,] map2, int value)
    {
        int[,] outMap = map1;

        //Use positions of 1s on map2 to put "value" on outMap, and return outMap
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map2[x, y] == 0 && outMap[x,y] == 1) //only replace land tiles with new tiles
                {
                    outMap[x, y] = value;
                }
            }
        }
        return outMap;
    }

    public void PrintMap(int[,] map)
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

    public int[,] GetMap()
    {
        return map;
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
