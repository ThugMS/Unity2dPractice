using UnityEngine;

public class MakeGrid : MonoBehaviour
{
    public float starRiteLevel = 0.1f;
    public float cazelineLevel = 0.2f;
    public float blockLevel = 0.35f;
    public float scale = 0.1f;
    public int size = 100;

    [SerializeField]
    private int randomSeed = 1;

    Cell[,] grid;

    private int resourceStar = 0;
    private int[] dx = { 1, -1, 0, 0 };
    private int[] dy = { 0, 0, 1, -1 };

    
    private void FindResource(float[,] noiseMap, bool[,] findMap, int x, int y)
    {
        Debug.Log("find");
        if(x>=size || x<0 || y>=size || y < 0)
        {
            return;
        }

        if (noiseMap[x, y] >= blockLevel)
            return;

        if (findMap[x,y] == true)
            return;

        findMap[x, y] = true;

        if (noiseMap[x,y] >= cazelineLevel)
        {
         
        }
        else
        {
            if(resourceStar == 0)
            {
                if (noiseMap[x,y] >= starRiteLevel)
                {
                    resourceStar = 1;
                }
                else
                {
                    resourceStar = 2;
                }
            }
            else if(resourceStar == 1)
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

    //private void BFS(float[,] noiseMap, bool[,] visitMap, int x, int y)
    //{
    //    Debug.Log("BFS");
    //    if (x >= size || x < 0 || y >= size || y < 0)
    //    {
    //        return;
    //    }

    //    if (noiseMap[x, y] >= blockLevel)
    //        return;

    //    if (visitMap[x, y] == true)
    //        return;

    //    visitMap[x, y] = true;

    //    if (noiseMap[x, y] >= cazelineLevel)
    //    {
    //        for (int i = 0; i < 4; i++)
    //        {
    //            BFS(noiseMap, visitMap, x + dx[i], y + dy[i]);
    //        }
    //    }
    //    else
    //    {
    //        if (resourceStar)
    //        {
    //            noiseMap[x, y] = starRiteLevel - 0.01f;
    //        }
    //        else
    //        {
    //            noiseMap[x, y] = starRiteLevel + 0.01f;
    //        }

    //        for (int i = 0; i < 4; i++)
    //        {
    //            BFS(noiseMap, visitMap, x + dx[i], y + dy[i]);
    //        }
    //    }
    //}

    private void Start()
    {
        Random.InitState(randomSeed);

        float[,] noiseMap = new float[size, size];

        bool[,] visitMap = new bool[size, size];
        bool[,] findMap = new bool[size, size];
        float xOffset = Random.Range(-10000f, 10000f);
        float yOffset = Random.Range(-10000f, 10000f);

        for (int y = 0; y < size; y++)
        {
            for(int x = 0; x < size; x++)
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

        for(int y = 0; y < size; y++)
        {
            for(int x = 0; x < size; x++)
            {   
                if (noiseMap[x,y] < blockLevel && findMap[x,y] != true)
                {
                    FindResource(noiseMap, findMap, x, y);
                    resourceStar = 0;
                }

            }
        }

        grid = new Cell[size, size];
        for(int y = 0; y < size; y++)
        {
            for(int x = 0; x < size; x++)
            {
                Cell cell = new Cell();
                float noiseValue = noiseMap[x,y];

                if (noiseValue < starRiteLevel)
                    cell.isStarLite = true;
                else if (noiseValue < cazelineLevel)
                    cell.isCazeline = true;
                else if (noiseValue < blockLevel)
                {
                    float sub = Random.Range(0.0f, 1.0f);

                    //if (sub < 1 / 5f)
                    //{
                    //    cell.isStarLite = true;
                    //}
                    //else if (sub < 2 / 5f)
                    //{
                    //    cell.isCazeline = true;
                    //}
                    //else
                    //{
                    //    cell.isBlock = true;
                    //}
                    cell.isBlock = true;

                }
                grid[x, y] = cell;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        for(int y = 0; y < size; y++)
        {
            for(int x = 0; x < size; x++)
            {
                Cell cell = grid[x, y];
                if (cell.isStarLite)
                    Gizmos.color = Color.blue;

                else if (cell.isCazeline)
                    Gizmos.color = Color.yellow;

                else if (cell.isBlock)
                    Gizmos.color = Color.gray;
                else
                    Gizmos.color = Color.black;

                Vector3 pos = new Vector3(x, y, 0);
                Gizmos.DrawCube(pos, Vector3.one);
            }
        }
    }
}
