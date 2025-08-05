using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCreating : MonoBehaviour
{
    [SerializeField] private MapGeneration mapGenerator;
    [SerializeField] private GameObject capitalPrefab;

    private int width;
    private int height;

    private int[,] map;
    private int[,] buildMap;

    public void GenerateCapital()
    {
        map = mapGenerator.GetMap();
        buildMap = GenerateEmptyMap();

        bool isValid = false;
        int neighb = 0;

        int counter = 0;
        while (!isValid)
        {
            //Infinite loop breaker just in case can't find a suitable spot for capital
            if (counter == 20)
            {
                Debug.Log("took too long to generate");
                break;
            }
            counter++;

            //Pick a random spot on the map for potential capital location
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            //Skip potential capital locations on the edge of the map
            if (x == 0 || x == width - 1 || y == 0 || y == height - 1) continue;

            // Only consider placing the capital on land (skip if not land)
            if (map[x, y] != 1) continue;

            //Make sure no more than 3 water tiles next to capital
            neighb = 0;
            BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);
            foreach (var b in myB.allPositionsWithin)
            {
                //Skip center square
                if (b.x == 0 && b.y == 0) continue;

                //b.x and b.y are offsets
                int checkX = x + b.x;
                int checkY = y + b.y;

                //Check within the barrier
                if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < height)
                {
                    if (map[checkX, checkY] == 0) // Count water tiles
                        neighb++;
                }
            }

            if (neighb <= 3)
            {
                buildMap[x, y] = 9; //9 = Capital
                Instantiate(capitalPrefab, new Vector2(x, y), Quaternion.identity, this.transform);
                Debug.Log("Found the capital: [" + x + ", " + y + "]");
                mapGenerator.PrintMap(buildMap);
                isValid = true;
            }
        }
        
    }
    private int[,] GenerateEmptyMap()
    {
        int[,] newMap = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                newMap[x, y] = 0;
            }
        }

        return newMap;
    }

    // Start is called before the first frame update
    void Start()
    {
        width = mapGenerator.width;
        height = mapGenerator.height;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
