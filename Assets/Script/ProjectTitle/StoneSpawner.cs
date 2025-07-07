using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public int cubeCount = 200;
    public float spacing = 2f; // Distance between cubes

    public GameObject[,] objectGrid;

    private void Start()
    {
        objectGrid = new GameObject[cubeCount, cubeCount];
        CreateOneLayer();
    }
    void CreateThreeLayer()
    {
        int gridSize = Mathf.CeilToInt(Mathf.Pow(cubeCount, 1f / 3f)); // Cube root for 3D grid
        int count = 0;

        for (int x = 0; x < gridSize && count < cubeCount; x++)
        {
            for (int y = 0; y < gridSize && count < cubeCount; y++)
            {
                for (int z = 0; z < gridSize && count < cubeCount; z++)
                {
                    Vector3 position = new Vector3(x, y, z) * spacing;
                    Instantiate(cubePrefab, position, Quaternion.identity);
                    count++;
                }
            }
        }
    }



    void DestroyFirstRow()
    {
        for (int col = 0; col < 5; col++)
        {
            GameObject obj = objectGrid[0, col];
            if (obj != null)
            {
                Destroy(obj);
            }
        }
    }

    void CreateOneLayer()
    {
        int gridSize = Mathf.CeilToInt(Mathf.Pow(cubeCount, 1f / 3f)); // Cube root for 3D grid
        int count = 0;
        int y = 1;

        for (int x = 0; x < gridSize && count < cubeCount; x++)
        {

            for (int z = 0; z < gridSize && count < cubeCount; z++)
            {
                Vector3 position = new Vector3(x, y, z) * spacing;
                GameObject clone = Instantiate(cubePrefab, position, Quaternion.identity);
                objectGrid[x, z] = clone;
                count++;

            }

        }
    }

    void DestoryFrontLine(int index)
    {
        for (int col = 0; col < 5; col++)
        {
            GameObject obj = objectGrid[index, col];
            if (obj != null)
            {
                Destroy(obj);
            }
        }
    }

    int count = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            DestoryFrontLine(count);
            count++;
        }
    }
}