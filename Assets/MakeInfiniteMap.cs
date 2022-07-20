using UnityEngine;

public class MakeInfiniteMap : MonoBehaviour
{
    [SerializeField]
    private GameObject starRitePrefab;
    [SerializeField]
    private GameObject cazelinePrefab;
    [SerializeField]
    private GameObject blockPrefab;

    public float starRiteLevel = 0.1f;
    public float cazelineLevel = 0.2f;
    public float blockLevel = 0.35f;
    public float scale = 0.1f;
    public int size = 50;

    [SerializeField]
    private int randomSeed = 1;

    Cell[,] grid;

    private int resourceStar = 0;
    private int[] dx = { 1, -1, 0, 0 };
    private int[] dy = { 0, 0, 1, -1 };

    private float posX;
    private float posY;



    private void FindResource(float[,] noiseMap, bool[,] findMap, int x, int y)
    {
        if (x >= size || x < 0 || y >= size || y < 0)
        {
            return;
        }

        if (noiseMap[x, y] >= blockLevel)
            return;

        if (findMap[x, y] == true)
            return;

        findMap[x, y] = true;

        if (noiseMap[x, y] >= cazelineLevel)
        {

        }
        else
        {
            if (resourceStar == 0)
            {
                if (noiseMap[x, y] >= starRiteLevel)
                {
                    resourceStar = 1;
                }
                else
                {
                    resourceStar = 2;
                }
            }
            else if (resourceStar == 1)
            {
                noiseMap[x, y] = starRiteLevel + 0.01f;
            }
            else
            {
                noiseMap[x, y] = starRiteLevel - 0.01f;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            FindResource(noiseMap, findMap, x + dx[i], y + dy[i]);
        }

    }


    public void NoiseCreateMap(int mapPosX, int mapPosY)
    {
        Random.InitState(randomSeed);

        float[,] noiseMap = new float[size, size];

        bool[,] visitMap = new bool[size, size];
        bool[,] findMap = new bool[size, size];
        float xOffset = Random.Range(-10000f, 10000f);
        float yOffset = Random.Range(-10000f, 10000f);

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float noiseValue = Mathf.PerlinNoise(x * scale + xOffset, y * scale + yOffset);
                noiseMap[x, y] = noiseValue;

                if (noiseValue < blockLevel && noiseValue >= cazelineLevel)
                {
                    float sub = Random.Range(0.0f, 1.0f);

                    if (sub < 1 / 5f)
                    {
                        noiseMap[x, y] = starRiteLevel - 0.001f;
                    }
                    else if (sub < 2 / 5f)
                    {
                        noiseMap[x, y] = starRiteLevel + 0.001f;
                    }
                    else
                    {
                    }
                }
            }
        }

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                if (noiseMap[x, y] < blockLevel && findMap[x, y] != true)
                {
                    FindResource(noiseMap, findMap, x, y);
                    resourceStar = 0;
                }

            }
        }

        grid = new Cell[size, size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Cell cell = new Cell();
                float noiseValue = noiseMap[x, y];

                if (noiseValue < starRiteLevel)
                    cell.isStarLite = true;
                else if (noiseValue < cazelineLevel)
                    cell.isCazeline = true;
                else if (noiseValue < blockLevel)
                {
                    float sub = Random.Range(0.0f, 1.0f);
                    cell.isBlock = true;

                }
                grid[x, y] = cell;
            }
        }

        CreateInitialMap(mapPosX, mapPosY);
    }

    private void Update()
    {

    }

    private void CreateInitialMap(int mapPosX, int mapPosY)
    {


        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Vector3 pos = new Vector3(x + (mapPosX * 50) - 25, y + (mapPosY * 50) - 25, 0);

                Cell cell = grid[x, y];
                if (cell.isStarLite)
                {
                    GameObject clone = Instantiate(starRitePrefab, pos, Quaternion.identity);
                }
                else if (cell.isCazeline)
                {
                    GameObject clone = Instantiate(cazelinePrefab, pos, Quaternion.identity);
                }

                else if (cell.isBlock)
                {
                    GameObject clone = Instantiate(blockPrefab, pos, Quaternion.identity);
                }

            }
        }
    }


}
