using System;
using UnityEngine;

public class ZombiSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public int cubeCount = 200;
    public float spacing = 2f; // Distance between cubes

    public GameObject[,] objectGrid;
    public Transform startPointd;


    public float radius = 2f;            // Radius around the point
    public LayerMask targetLayer;        // Optional: filter by layer
    int count = 0;
    void FindAndRemove(Vector3 pos, float myRadius)
    {
        Vector3 point = pos;
        float radius = myRadius;

        Collider[] hits = Physics.OverlapSphere(point, radius, targetLayer);

        foreach (Collider col in hits)
        {
            GameObject obj = col.gameObject;
            Destroy(obj);
        }
    }

    private void Start()
    {
        TargetStone.OnKnockDownEvent += TargetStone_OnKnockDownEvent;
        TargetStone.OnHitByProjectile += TargetStone_OnHitByProjectile;

        objectGrid = new GameObject[cubeCount, cubeCount];
        SpawnZombi();
    }

    private void TargetStone_OnHitByProjectile(StoneType obj)
    {
        DestoryFrontLine(count);
        count++;
    }

    private void TargetStone_OnKnockDownEvent(StoneType obj)
    {
        GameObject target = GameObject.FindWithTag("Target");

        Debug.Log(target.transform.position);
        FindAndRemove(target.transform.position, 30);
    }



    public void SpawnZombi()
    {
        objectGrid = new GameObject[cubeCount, cubeCount];
        int gridSize = Mathf.CeilToInt(Mathf.Pow(cubeCount, 1f / 3f)); // Cube root for 3D grid
        int y = 1;

        for (int x = 0; x < 50; x++)
        {
            for (int z = 0; z < 50; z++)
            {
                Vector3 position = new Vector3(x + 28, y, z - 22) * spacing;
                GameObject clone = Instantiate(cubePrefab, position, Quaternion.identity);
                objectGrid[x, z] = clone;

            }

        }
    }

    void DestoryFrontLine(int index)
    {
        for (int col = 0; col < 50; col++)
        {
            GameObject obj = objectGrid[index, col];
            if (obj != null)
            {
                Destroy(obj);
            }
        }
    }

}

