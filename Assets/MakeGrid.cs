using UnityEngine;

public class MakeGrid : MonoBehaviour
{
    public float waterLevel = 0.4f;
    public float scale = 0.1f;
    public int size = 100;

    [SerializeField]
    private int randomSeed = 1;

    Cell[,] grid;

    private void Start()
    {
        Random.InitState(randomSeed);

        float[,] noiseMap = new float[size, size];
        float xOffset = Random.Range(-10000f, 10000f);
        float yOffset = Random.Range(-10000f, 10000f);

        for (int y = 0; y < size; y++)
        {
            for(int x = 0; x < size; x++)
            {
                float noiseValue = Mathf.PerlinNoise(x * scale + xOffset, y * scale + yOffset);
                noiseMap[x, y] = noiseValue;
            }
        }

        grid = new Cell[size, size];
        for(int y = 0; y < size; y++)
        {
            for(int x = 0; x < size; x++)
            {
                Cell cell = new Cell();
                float noiseValue = noiseMap[x,y];
                cell.isWater = noiseValue < waterLevel;
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
                if (cell.isWater)
                    Gizmos.color = Color.blue;

                else
                    Gizmos.color = Color.green;

                Vector3 pos = new Vector3(x, y, 0);
                Gizmos.DrawCube(pos, Vector3.one);
            }
        }
    }
}
