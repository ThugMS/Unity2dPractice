using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayerController : MonoBehaviour
{

    bool[,] isCreate = new bool[1000, 1000];
    Vector3 lastMoveDirection = Vector3.right;
    private float moveSpeed = 15.0f;

    void Start()
    {
        isCreate[500, 500] = true;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(x, y, 0) * moveSpeed * Time.deltaTime;

        if (x != 0 || y != 0)
        {
            lastMoveDirection = new Vector3(x, y, 0);
        }

        float playerPosX = transform.position.x;
        float playerPosY = transform.position.y;

        int mapPosX = (int)((playerPosX + 25) / 50) + 500;
        int mapPosY = (int)((playerPosY + 25) / 50) + 500;

        if (playerPosX < -24) mapPosX--;
        if (playerPosY < -24) mapPosY--;

        float centerMapX = (mapPosX - 500) * 50f;
        float centerMapY = (mapPosY - 500) * 50f;

        if (Mathf.Abs(playerPosX - centerMapX) > 15)
        {
            if (playerPosX - centerMapX > 0)
            {
                mapPosX++;
            }
            if (playerPosX - centerMapX < 0)
            {
                mapPosX--;
            }

            if (!isCreate[mapPosX, mapPosY])
            {
                Debug.Log(mapPosX);
                Debug.Log(mapPosY);

                MakeInfiniteMap makeMap = GetComponent<MakeInfiniteMap>();
                isCreate[mapPosX, mapPosY] = true;

                makeMap.NoiseCreateMap(mapPosX - 500, mapPosY - 500);
            }

            if (playerPosX - centerMapX > 0)
            {
                mapPosX--;
            }
            if (playerPosX - centerMapX < 0)
            {
                mapPosX++;
            }
        }

        if (Mathf.Abs(playerPosY - centerMapY) > 15)
        {

            if (playerPosY - centerMapY > 0)
            {
                mapPosY++;
            }
            if (playerPosY - centerMapY < 0)
            {
                mapPosY--;
            }

            if (!isCreate[mapPosX, mapPosY])
            {
                Debug.Log(mapPosX);
                Debug.Log(mapPosY);

                MakeInfiniteMap makeMap = GetComponent<MakeInfiniteMap>();
                isCreate[mapPosX, mapPosY] = true;

                makeMap.NoiseCreateMap(mapPosX - 500, mapPosY - 500);
            }

        }
    }
}
